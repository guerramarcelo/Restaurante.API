using Restaurante.Classes;

namespace Restaurante.API.Inputs
{
    public class ComandaInput 
    {       
        public Guid Id { get; set; }
        public int NrMesa { get; set; }
        public bool Aberta { get; set; }
        public Atendente Atendente { get; set; }
        public decimal ValorPago { get; set; }
        public Pedido Pedido{ get; set; }
        public decimal ValorTotal { get; set; }
               
    }
}
