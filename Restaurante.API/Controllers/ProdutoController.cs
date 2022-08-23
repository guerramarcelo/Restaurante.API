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
                Produto produto = new(input.Nome, input.Preco, input.Tipo);
                var parameters = new { nome = produto.Nome, preco = produto.Preco, tipo = produto.Tipo };
                var sql = "INSERT INTO [Produto] (Nome, Preco, Tipo) VALUES (@nome, @preco, @tipo)";
                var resultado = connection.Query(sql, parameters);
                return Ok(resultado);
            }
        }

        [HttpGet("ObterProdutos")]
        public ActionResult ObterProdutos()
        {           
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM [Produto]";
                var resultado = connection.Query<Produto>(sql);                      
                return Ok(resultado);
            }           
        }

        [HttpGet("{id}/ObterProduto")]
        public ActionResult ObterProduto(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                var sql = "select * from [Produto] where Id = @id ";
                var resultado = connection.Query<Produto>(sql, parameters);
                return Ok(resultado);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduto(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                var sql = "DELETE FROM [Produto] WHERE Id = @id";
                var resultado = connection.Execute(sql, parameters);
                return Ok (resultado);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduto(Guid id, [FromBody] ProdutoInput input)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id, nome = input.Nome, preco = input.Preco, tipo = input.Tipo};
                var sql = "UPDATE [Produto] SET Nome = @nome, Preco = @preco, Tipo = @tipo WHERE Id = @id";
                var resultado = connection.Query<Produto>(sql, parameters);
                return Ok(resultado);
            }
        }

    }
}
