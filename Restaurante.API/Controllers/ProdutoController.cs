using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Inputs;
using Restaurante.API.Outputs;
using Restaurante.Classes;

namespace RestauranteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProdutoController : Controller
    {
        public static List<Produto> produtos;
        public ProdutoController()
        {
            if (produtos == null)
                produtos = new List<Produto>();
        }

        [HttpPost()]
        public ActionResult CriarProduto(ProdutoInput input)
        {
            var errorOutput = new ErrorOutput();

            if (string.IsNullOrEmpty(input.Nome))
                errorOutput.AddError("Nome é obrigatório");

            if ((input.Preco == 0.0M))
                errorOutput.AddError("Preço é obrigatório");

            if (input.Tipo == null)
                errorOutput.AddError("Tipo é obrigatório");

            if (errorOutput.HasErrors)
                return BadRequest(errorOutput);

            var produto = new Produto(input.Nome, input.Preco, input.Tipo);
            produtos.Add(produto);
            return Ok(produto);
        }


        [HttpGet("{id}")]
        public ActionResult GetProduto(Guid id)
        {
            var produto = produtos.FirstOrDefault(produto => produto.Id == id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }


        [HttpGet()]
        public ActionResult GetProduto()
        {
            if (produtos == null || !produtos.Any())
                return NoContent();

            return Ok(produtos);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduto(Guid id)
        {
            var produto = produtos.FirstOrDefault(produto => produto.Id == id);
            return Ok(produto);
        }
    }    
}
