using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Inputs;
using Restaurante.API.Outputs;
using static Restaurante.Classes.Produto;
using Dapper;
using Restaurante.Classes;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;


namespace RestauranteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProdutoController : Controller
    {
        string connectionString = (@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestauranteDataBase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public ProdutoController()
        {

        }

        [HttpPost()]
        public ActionResult CriarProduto(ProdutoInput input)
        {            
            string query = "insert into [Produto] (Nome, Preco, Tipo) values(@Nome, @Preco, @Tipo)";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Nome", input.Nome);
            cmd.Parameters.AddWithValue("@Preco", input.Preco);
            cmd.Parameters.AddWithValue("@Tipo", input.Tipo);
            cmd.ExecuteNonQuery();

            var errorOutput = new ErrorOutput();
            if (string.IsNullOrEmpty(input.Nome))
                errorOutput.AddError("Nome é obrigatório");

            if ((input.Preco == 0.0M))
                errorOutput.AddError("Preço é obrigatório");

            if (input.Tipo < 0)
                errorOutput.AddError("Tipo é obrigatório");

            if (errorOutput.HasErrors)
                return BadRequest(errorOutput);           

            return Ok();
        }


        [HttpGet()]
        public ActionResult GetProdutos()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT * from [Produto]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader data = cmd.ExecuteReader();
            StringBuilder newString = new StringBuilder();

            foreach (var item in data)
            {
                newString.AppendFormat("Id: {0}, Nome: {1}, Preco: {2}, Tipo: {3}", data["Id"], data["Nome"], data["Preco"], data["Tipo"]);
                newString.AppendLine();
            }
            return Ok(newString.ToString());
        }

        [HttpGet("id")]
        public ActionResult GetProduto(Guid id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT * from [Produto] WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader data = cmd.ExecuteReader();
            StringBuilder newString = new StringBuilder();

            foreach (var item in data)
            {
                newString.AppendFormat("Id: {0}, Nome: {1}, Preco: {2}, Tipo: {3}", data["Id"], data["Nome"], data["Preco"], data["Tipo"]);
                newString.AppendLine();
            }
            return Ok(newString.ToString());
        }

        [HttpDelete("id")]
        public ActionResult DeleteProduto(Guid id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "DELETE FROM [Produto] WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            return Ok();
        }


        [HttpPut("id")]
        public ActionResult UpdateProduto(Guid id, [FromBody] ProdutoInput input)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "UPDATE [Produto] SET Nome = @Nome, Preco = @Preco, Tipo = @Tipo WHERE Id = @Id";
            SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@Nome", input.Nome);
            cmd.Parameters.AddWithValue("@Preco", input.Preco);
            cmd.Parameters.AddWithValue("@Tipo", input.Tipo);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            return Ok();
        }

    }
}
