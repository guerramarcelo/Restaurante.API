using Microsoft.AspNetCore.Mvc;
using Outputs;
using Restaurante.Classes;
using System.Text;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComandaController : Controller
    {
        //static List<Pedido> pedidos;
        static List<Comanda> comandas = new();
        static List<Atendente> atendentes = new();

        public ComandaController()
        {
            if (comandas == null)
                comandas = new List<Comanda>();


            if (atendentes == null)
                atendentes = new List<Atendente>();
        }


        [HttpPost("Atendente")]
        public ActionResult CriarAtendente(Atendente input)
        {
            var errorOutput = new ErrorOutput();
            if (string.IsNullOrEmpty(input.Nome))
                errorOutput.AddError("Nome é obrigatório");

            if (string.IsNullOrEmpty(input.Cpf))
                errorOutput.AddError("CPF é obrigatório");

            if (input.Salario == 0)
                errorOutput.AddError("Salário deve ser maior que ZERO");

            var atendente = new Atendente(input.Nome, input.Cpf, input.Salario);

            atendentes.Add(atendente);
            return Ok(atendente);

        }



        [HttpPost()]
        public ActionResult CriarComanda(Comanda input)
        {
            var errorOutput = new ErrorOutput();

            if (input.nrMesa == 0)
                errorOutput.AddError("Numero da mesa é obrigatório");

            if (input.atendente == null)
                errorOutput.AddError("Atendente é obrigatório");

            if (errorOutput.HasErrors)
                return BadRequest(errorOutput);


            var comanda = new Comanda(input.nrMesa, input.atendente);

            comandas.Add(comanda);
            comanda.AbrirComanda();
            return Ok(comanda);

        }


        [HttpGet("{id}")]
        public ActionResult GetComanda(Guid id)
        {
            var comanda = comandas.FirstOrDefault(comanda => comanda.Id == id);

            if (comandas == null || comandas.Any())
                return NotFound();


            return Ok(comanda);

        }



    }

}
