using Dapper;
using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Inputs;
using Restaurante.Classes;
using System.Data.SqlClient;
using System.Text;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComandaController : Controller
    {

        string connectionString = (@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestauranteDataBase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        [HttpGet("Atendentes")]
        public ActionResult GetAtendentes()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM [Atendente]";
                var atendentes = connection.Query<AtendenteInput>(sql);
                return Ok(atendentes);
            }
        }

        [HttpGet()]
        public ActionResult GetComandas()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM [Comanda]";
                var comandas = connection.Query<ComandaInput>(sql);
                return Ok(comandas);
            }
        }

        [HttpPost]
        public ActionResult AbrirComanda(ComandaInput input)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { nrMesa = input.NrMesa, atendenteId = input.AtendenteId, aberta = input.Aberta };
                var sql = "INSERT INTO [Comanda] (NrMesa, AtendenteId, Aberta) VALUES (@nrMesa, @atendenteId, @aberta)";
                var result = connection.Query(sql, parameters);
                return Ok(result);
            }
            
        }

        [HttpGet("Pedidos")]
        public ActionResult GetPedidos()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM [Pedido]";
                var pedidos = connection.Query<PedidoInput>(sql);
                return Ok(pedidos);
            }
        }


        [HttpPost("{id}/Pedido")]
        public ActionResult CriarPedido(Guid id, PedidoInput input)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Hora = input.Hora, comandaId = id};
                var sql = "INSERT INTO [Pedido] (Hora, ComandaId) VALUES (@hora, @comandaId)";
                var result = connection.Query(sql, parameters);
                return Ok();
            }                        
        }

        [HttpPost("Pedido/Itens")]
        public ActionResult AdicionarItens(ItemPedidoInput input)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { quantidade = input.Quantidade, produtoId = input.ProdutoId, pedidoId = input.PedidoId };
                var sql = "INSERT INTO [ItemPedido] (Quantidade, ProdutoId, PedidoId) VALUES (@quantidade, @produtoId, @pedidoId)";
                var result = connection.Query(sql, parameters);
                return Ok();
            }
        }

        [HttpPut("{id}/Pedido/Pagamento")]
        public ActionResult EfetuarPagamento (Guid id, PagamentoInput pagamento)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id , valorPago = pagamento.ValorPago};
                var sql = "UPDATE [Comanda] SET ValorPago = @valorPago WHERE Id = @id";
                var result = connection.Query(sql, parameters);
                return Ok();
            }
        }

        [HttpPost("{id}/Fechar")]
        public ActionResult FecharComanda (Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                var sql = "UPDATE [Comanda] SET Aberta = 0  WHERE Id = @id";
                var result = connection.Query(sql, parameters);
                return Ok();
            }            
        }

    }
}

