using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Inputs;
using Restaurante.API.Outputs;
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
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT * from [Atendente]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader data = cmd.ExecuteReader();
            StringBuilder newString = new StringBuilder();

            foreach (var item in data)
            {
                newString.AppendFormat("Id: {0}, Nome: {1}, Cpf: {2}, Salario: {3}", data["Id"], data["Nome"], data["Cpf"], data["Salario"]);
                newString.AppendLine();
            }
            return Ok(newString.ToString());
        }

        [HttpGet()]
        public ActionResult GetComandas()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT * from [Comanda]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader data = cmd.ExecuteReader();
            StringBuilder newString = new StringBuilder();

            foreach (var item in data)
            {
                newString.AppendFormat("Id: {0}, NrMesa: {1}, AtendenteId: {2}", data["Id"], data["NrMesa"], data["AtendenteId"]);
                newString.AppendLine();
            }
            return Ok(newString.ToString());
        }

        [HttpPost]
        public ActionResult AbrirComanda(ComandaInput input)
        {
            string query = "INSERT INTO [Comanda] (NrMesa, AtendenteId, Aberta) values (@NrMesa, @AtendenteId, @Aberta)";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@NrMesa", input.NrMesa);
            cmd.Parameters.AddWithValue("@AtendenteId", input.AtendenteId);
            cmd.Parameters.AddWithValue("@Aberta", input.Aberta);
            cmd.ExecuteNonQuery();
            return Ok();
        }

        [HttpGet("Pedidos")]
        public ActionResult GetPedidos()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT * from [Pedido]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader data = cmd.ExecuteReader();
            StringBuilder newString = new StringBuilder();

            foreach (var item in data)
            {
                newString.AppendFormat("Id: {0}, Hora: {1}, ComandaId: {2}", data["Id"], data["Hora"], data["ComandaId"]);
                newString.AppendLine();
            }
            return Ok(newString.ToString());
        }



        [HttpPost("{id}/Pedido")]
        public ActionResult CriarPedido(Guid id, PedidoInput input)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO [Pedido] (Hora, ComandaId) values (@Hora, @ComandaId)";
            cmd.Parameters.AddWithValue("@Hora", input.Hora);
            cmd.Parameters.AddWithValue("@ComandaId", id);
            cmd.ExecuteNonQuery();
            return Ok();
        }

        [HttpPost("Pedido/Itens")]
        public ActionResult AdicionarItens(ItemPedidoInput input)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = @"INSERT INTO [ItemPedido] (Quantidade, ProdutoId, PedidoId) values (@Quantidade, @ProdutoId, @PedidoId)";
            cmd.Parameters.AddWithValue("@Quantidade", input.Quantidade);
            cmd.Parameters.AddWithValue("@ProdutoId", input.ProdutoId);
            cmd.Parameters.AddWithValue("@PedidoId", input.PedidoId);
            cmd.ExecuteNonQuery();
            return Ok();
        }

        [HttpPut("{Id}/Pedido/Pagamento")]
        public ActionResult EfetuarPagamento (Guid id, PagamentoInput pagamento)
        {            
            SqlConnection con = new(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();            
            cmd.CommandText = "UPDATE [Comanda] SET ValorPago = @ValorPago WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@ValorPago", pagamento);
            cmd.Parameters.AddWithValue("@Id", id);
            return Ok();
        }

        [HttpPost("{Id}/Fechar")]
        public ActionResult FecharComanda (Guid id)
        {
            SqlConnection con = new(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE [Comanda] SET (Aberta = 0) WHERE Id = @Id";            
            return Ok();
            //falta validação!!!!!!!!
        }


    }


}

