using Dapper;
using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Inputs;
using Restaurante.Classes;
using System.Data.SqlClient;
using System.Text.Json;

namespace Restaurante.API.Controllers
{
    //----------- COMANDAS
    [ApiController]
    [Route("[controller]")]
    public class ComandaController : Controller
    {
        string connectionString = (@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestauranteDataBase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        //[HttpPost("NovoAtendente")]
        //public ActionResult NovoAtendente(ComandaInput input)
        //{            
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        Atendente atendente = new(input.Atendente.Nome, input.Atendente.Cpf, input.Atendente.Salario, input.Atendente.PorcentagemComissao);
        //        var parameters = new {Nome = atendente.Nome, Cpf = atendente.Cpf, Salario = atendente.Salario, PorcentagemComissao = atendente.PorcentagemComissao };
        //        var sql = "INSERT INTO Atendente (Nome, Cpf, Salario, PorcentagemComissao) VALUES (@Nome, @Cpf, @Salario, @PorcentagemComissao)";
        //        var resultado = connection.Query<Atendente>(sql, parameters);
        //        return Ok(atendente);
        //    }
        //}

        [HttpGet("Atendentes")]
        public ActionResult GetAtendentes()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM [Atendente]";
                var atendentes = connection.Query<Atendente>(sql);
                return Ok(atendentes);
            }
        }

        [HttpPost("/NovaComanda")]                                      //Aqui é uma comanda que acabou de ser aberta, portanto, vazia.
        public ActionResult NovaComanda(int nrMesa, Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                Comanda comanda = new(nrMesa);
                var parameters = new { Id = comanda.Id, NrMesa = nrMesa, AtendenteId = id, Aberta = comanda.AbrirComanda() };
                var sql = "Insert into Comanda (NrMesa, AtendenteId, Aberta) values (@NrMesa, @AtendenteId, @Aberta)";
                var resultado = connection.Query<Comanda>(sql, parameters);
                return Ok(comanda);

            }
        }

        [HttpGet("/ObterComandas")]

        public ActionResult ObterComandas()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "Select c.Id, c.NrMesa, c.Aberta, c.ValorPago, a.Id, a.Nome, a.Cpf, a.Salario, a.PorcentagemComissao, p.Id, p.Hora, i.Id, i.Quantidade, pp.Id, pp.Nome, pp.Preco, pp.Tipo " +
                    "FROM Comanda c " +
                    "INNER JOIN Atendente a ON c.AtendenteId = a.Id " +
                    "INNER JOIN Pedido p ON p.ComandaId = c.Id " +
                    "INNER JOIN ItemPedido i ON i.PedidoId = p.Id " +
                    "INNER JOIN Produto pp ON i.ProdutoId = pp.Id";

                var resultado = connection.Query<ComandaInput, Atendente, Pedido, ItemPedido, Produto, ComandaInput>(sql, (c, a, p, i, pp)
                    =>
                {
                    c.Atendente = a; i.Produto = pp; c.Pedido = p; c.Pedido.AdicionarItemPedido(i);
                    c.ValorTotal = c.Pedido.ValorPedido(); c.Atendente.PorcentagemComissao = c.ValorTotal * 0.1M;
                    return c;
                }, splitOn: "Id, Id, Id, Id, Id").AsQueryable();
                return Ok(resultado);
            }
        }
        
        [HttpDelete("RemoverComanda/{id}")]
        public ActionResult RemoverComanda(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                var sql = "DELETE FROM [Comanda] WHERE @Id = Id";
                var resultado = connection.Query<Comanda>(sql, parameters);
                return Ok();
            }
        }


        [HttpPost ("{id}/EfetuarPagamento")]
        public ActionResult EfetuarPagamento(Guid id, PagamentoInput input)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id, ValorPago = input.ValorPago };
                var sql = "UPDATE Comanda SET ValorPago = @ValorPago " +
                     "WHERE Id = @Id";
                var resultado = connection.Query<ComandaInput>(sql, parameters);
                return Ok(resultado);
            }
        }

        [HttpPut("{id}/FecharComanda")] 
        public ActionResult FecharComanda(Guid id)
        {
            using (var connection = new SqlConnection (connectionString))
            {
                var sql = "UPDATE Comanda SET Aberta = 0 WHERE Id = @Id AND ValorPago >= ValorTotal; ";
                var resultado = connection.Query<Comanda>(sql, new {Id = id});
                return Ok(resultado);
            }
        }
    }

    //----------- PEDIDOS TALVEZ TENHA QUE ADICIONAR UMA LISTA DE ITEMPEDIDO, COM UM APENAS ESTÁ FUNCIONANDO

    [ApiController]
    [Route("[controller]")]
    public class PedidoController : Controller
    {
        string connectionString = (@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestauranteDataBase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        [HttpPost("{id}/NovoPedido")]
        public ActionResult NovoPedido(Guid id)                  //Aqui é um pedido que acabou de ser feito, portanto, vazio.
        {
            Pedido pedido = new Pedido();
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Hora = pedido.HoraPedido, comandaId = id };
                var sql = "INSERT INTO [Pedido] (Hora, ComandaId) VALUES (@hora, @comandaId)";
                var resultado = connection.Query<Pedido>(sql, parameters);
                return Ok(pedido);
            }
        }

        [HttpGet("ObterPedidos")]
        public ActionResult ObterPedidos()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT p.Id, p.Hora, p.ComandaId, i.Id, i.Quantidade, pp.Id, pp.Nome, pp.Preco, pp.Tipo " +
                    "FROM Pedido p " +
                    "INNER JOIN ItemPedido i ON i.PedidoId = p.Id " +
                    "INNER JOIN Produto pp ON i.ProdutoId = pp.Id ";
                var result = connection.Query<PedidoInput, ItemPedido, Produto, PedidoInput>(sql, (p, i, pp) => { p.Produto = i; i.Produto = pp; return p; }, splitOn: "Id, Id").AsQueryable();
                return Ok(result);
            }
        }

        [HttpDelete("RemoverPedido/{id}")]
        public ActionResult RemoverPedido(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                var sql = "DELETE FROM [Pedido] WHERE @Id = Id";
                var resultado = connection.Query<Pedido>(sql, parameters);
                return Ok();
            }
        }
    }


    //----------- ITEMPEDIDOS COMPLETO

    [ApiController]
    [Route("[controller]")]
    public class ItemPedidoController : Controller
    {
        string connectionString = (@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestauranteDataBase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        [HttpPost("{id}/CriarItemPedido")]
        public ActionResult CriarItemPedido(Guid produtoid, Guid id, int Quantidade = 1)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                ItemPedido item = new ItemPedido();
                var parameters = new { PedidoId = id, ProdutoId = produtoid, Quantidade = Quantidade, Id = item.Id };
                var sql = "INSERT INTO [ItemPedido] (ProdutoId, PedidoId, Quantidade) VALUES (@ProdutoId, @PedidoId, @Quantidade)";
                var resultado = connection.Query<ItemPedido>(sql, parameters);
                return Ok(resultado);
            }
        }


        //[HttpPost("NovoPedido/{id}/AdicionarItens")]
        //public ActionResult AdicionarItens(Guid id, ItemPedidoInput input)
        //{
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        ItemPedido item = new(input.Produto, input.Quantidade);
        //        var parameters = new { quantidade = item.Quantidade, produtoid = item.Produto.Id };
        //        var sql = "INSERT INTO [ItemPedido] (Quantidade, ProdutoId) VALUES (@quantidade, @produtoId)";
        //        var result = connection.Query(sql, parameters);
        //        return Ok();
        //    }
        //}

        [HttpGet("ObterItens")]
        public ActionResult ObterItens()
        {
            using var connection = new SqlConnection(connectionString);
            {
                var sql = "SELECT i.Id, i.Quantidade, p.Id, p.Nome, p.Tipo, p.Preco " +
                    "FROM Produto p " +
                    "INNER JOIN [ItemPedido] i on p.Id = i.ProdutoId";

                var resultado = connection.Query<ItemPedidoInput, Produto, ItemPedidoInput>(sql, (i, p) => { i.Produto = p; return i; }, splitOn: "Id, Id").AsQueryable();

                return Ok(resultado);
            }
        }


        [HttpDelete("RemoverItem")]
        public ActionResult RemoverItem(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "DELETE FROM ItemPedido WHERE Id = @Id";
                var resultado = connection.Query<ItemPedido>(sql, new { Id = id });
                return Ok(resultado);
            }
        }

    }
}


