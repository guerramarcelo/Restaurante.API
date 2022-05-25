using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Inputs;
using Restaurante.API.Outputs;
using Restaurante.Classes;
using RestauranteAPI.Controllers;
using System.Text;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComandaController : Controller
    {
        static List<Comanda> comandas = new();

        public ComandaController()
        {
            if (comandas == null)
                comandas = new List<Comanda>();
        }

        [HttpPost]
        public ActionResult AbrirComanda(ComandaInput input)
        {
            var errorOutput = new ErrorOutput();
            if (input.NrMesa <= 0)
                errorOutput.AddError("Numero da mesa deve ser maior que ZERO!");

            if (input.Atendente == null)
                errorOutput.AddError("É necessário o Id do atendente!");

            var comanda = new Comanda(input.NrMesa, new Atendente(input.Atendente.Nome, input.Atendente.Cpf, input.Atendente.salario));
            comanda.AbrirComanda();
            comandas.Add(comanda);
            return Ok(comanda);
        }

        [HttpGet("{id}")]
        public ActionResult GetComanda(Guid id)
        {
            var comanda = comandas.FirstOrDefault(x => x.Id == id);
            return Ok(comanda);

        }


        [HttpPost("{id}/Pedido")]
        public ActionResult AdicionarPedidos(Guid id, [FromBody] PedidoInput pedidoInput)
        {
            var comanda = comandas.FirstOrDefault(x => x.Id == id);
            var pedido = new Pedido();

            foreach (var item in pedidoInput.Items)
            {
                var produto = ProdutoController.produtos.FirstOrDefault(prd => prd.Id == item.ProdutoId);
                ItemPedido itemPedido = new ItemPedido(produto, item.Quantidade);
                pedido.AdicionarItemPedido(itemPedido);
            }

            comanda.AdicionarPedido(pedido);
            return Ok(pedido);
        }

        [HttpPost("/{id}/Pagamento")]
        public ActionResult EfetuarPagamento(Guid id, [FromBody] PagamentoInput pagamento)
        {
            var comanda = comandas.FirstOrDefault(x => x.Id == id);
            var errorOutput = new ErrorOutput();
            comanda.EfetuarPagamento(pagamento.Valor);
            return Ok(comanda);
        }

        [HttpPost("/{id}/Fechar")]
        public ActionResult FecharComanda(ComandaInput input)
        {
            input.Aberta = false;
            return Ok();
        }

        [HttpGet("/{id}/Imprimir")]
        public ActionResult ImprimirComanda(Guid id)
        {
            var comanda = comandas.FirstOrDefault(x => x.Id == id);
            return Ok(comanda.ToString());
        }
    }
}
