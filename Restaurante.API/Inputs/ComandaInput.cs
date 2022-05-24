using Restaurante.Classes;

namespace Restaurante.API.Inputs
{
    public class ComandaInput
    {
        private readonly List<Atendente> atendentes = new()
        {
            new Atendente { Id = 1, Nome = "Marcelo", Cpf = "1234", Salario = 1000.0M },
            new Atendente { Id = 2, Nome = "Bruno", Cpf = "4321", Salario = 2000.0M },
            new Atendente { Id = 3, Nome = "Felipe", Cpf = "2468", Salario = 3000.0M }
        };


        public Guid Id { get; set; }
        
        public int NrMesa { get; set; }
        public decimal ValorPago { get; set; }

        public bool Aberta { get; set; }

        public Atendente atendente { get; set; }

       


        public IEnumerable<Atendente> GetAtendentes()
        {
            return atendentes;
        }

        public Atendente GetAtendente(int id)
        {
            atendente = atendentes.Where(x => x.Id == id).FirstOrDefault();

            return atendente;

        }



    }

}
