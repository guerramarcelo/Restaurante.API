using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Inputs;
using Restaurante.API.Outputs;
using static Restaurante.Classes.Produto;
using Dapper;
using Restaurante.Classes;
using System.Data.SqlClient;
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
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { nome = input.Nome, preco = input.Preco, tipo = input.Tipo };
                var sql = "INSERT INTO [Produto] (Nome, Preco, Tipo) VALUES (@nome, @preco, @tipo)";
                var result = connection.Query(sql, parameters);
                return Ok();
            }
        }

        [HttpGet()]
        public ActionResult GetProdutos()
        {           
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM [Produto]";
                var produtos = connection.Query<Produto>(sql);                      
                return Ok(produtos);
            }           
        }

        [HttpGet("id")]
        public ActionResult GetProduto(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                var sql = "select * from [Produto] where Id = @id ";
                var result = connection.Query(sql, parameters);
                return Ok(result);
            }
        }

        [HttpDelete("id")]
        public ActionResult DeleteProduto(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                var sql = "DELETE FROM [Produto] WHERE Id = @id";
                var affectedRows = connection.Execute(sql, parameters);
                return Ok (affectedRows);
            }
        }

        [HttpPut("id")]
        public ActionResult UpdateProduto(Guid id, [FromBody] ProdutoInput input)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id, nome = input.Nome, preco = input.Preco, tipo = input.Tipo};
                var sql = "UPDATE [Produto] SET Nome = @nome, Preco = @preco, Tipo = @tipo WHERE Id = @id";
                var result = connection.Query(sql, parameters);
                return Ok();
            }
        }

    }
}
