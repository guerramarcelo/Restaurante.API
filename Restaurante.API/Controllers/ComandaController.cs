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
        private readonly ComandaInput comandaInput;
        static List<Comanda> comandas = new();
        static List<Produto> produtos = new();
        static List <Pedido> pedidos = new();
        

        public ComandaController()
        {
            comandaInput = new ComandaInput();
            if (comandas == null)
                comandas = new List<Comanda>();

            if (produtos == null)
                produtos = new List<Produto>();

            if (pedidos == null)
                pedidos = new List<Pedido>();
        }

        [HttpPost]
        public ActionResult AbrirComanda(ComandaInput input)
        {
            var errorOutput = new ErrorOutput();
            input.atendente = comandaInput.GetAtendentes().FirstOrDefault(x => x.Id == input.atendente.Id);

            if (input.NrMesa <= 0)
                errorOutput.AddError("Numero da mesa deve ser maior que ZERO!");

            if (input.atendente == null)
                errorOutput.AddError("É necessário o Id do atendente!");


            var comanda = new Comanda(input.NrMesa, input.atendente);

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


        [HttpPost ("{id}/Pedido")]
        public ActionResult AdicionarPedidos (ProdutoInput input, int quantidade)
        {

            var produto = produtos.FirstOrDefault(x => x.Id == input.Id);

            ItemPedido itemPedido = new ItemPedido(produto, quantidade);

            Pedido pedido = new Pedido();
            pedido.AdicionarItemPedido(itemPedido);
            pedidos.Add(pedido);

             return Ok(pedido);
        }

        [HttpPost ("/{id}/Pagamento")]
        public ActionResult EfetuarPagamento (ComandaInput input, [FromBody]decimal valor)
        {
            var errorOutput = new ErrorOutput();

            input.ValorPago += valor;
                                    
            if (input.ValorPago <= 0)
                errorOutput.AddError("Valor deve ser maior que zero!");


            var valorTotal = pedidos.Sum(pedido => pedido.ValorPedido());

            input.atendente.PorcentagemComissao = (valorTotal * 0.1M);
            var valorFinal = valorTotal + input.atendente.PorcentagemComissao;
            
            
            if (input.ValorPago >= valorFinal)
                Console.WriteLine($"Troco = {input.ValorPago - valorFinal}");
         
            if (input.ValorPago <= valorFinal)
                errorOutput.AddError("Valor incompleto!");

            return Ok();
                        
        }

        [HttpPost ("/{id}/Fechar")]
        public ActionResult FecharComanda(ComandaInput input)
        {
            input.Aberta = false;

            return Ok();
        }

        [HttpGet("/{id}/Imprimir")]
        public ActionResult ImprimirComanda(Guid id)
        {
            var comanda = comandas.FirstOrDefault(comandas => comandas.Id == id);

            StringBuilder newString = new StringBuilder();

            newString.Append('-', 20);
            newString.AppendLine();

            foreach (var item in pedidos)
            {
                foreach (var item2 in item.Produtos)
                {

                    newString.AppendFormat("{0} - {1} - {2}", item2.Produto.Nome, item2.Quantidade, item2.ValorItemPedido());
                    newString.AppendLine();

                }

            }
            newString.Append('-', 20);
            newString.AppendLine();
            newString.AppendLine("Valor: " +  comanda.ValorFinal());
            newString.AppendLine("Valor Pago: " + comanda.ValorPago);

            return Ok(comanda);  

        }





    }
}
