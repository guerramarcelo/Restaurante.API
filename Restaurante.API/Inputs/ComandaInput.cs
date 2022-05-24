using Restaurante.Classes;

namespace Restaurante.API.Inputs
{
    public class ComandaInput
    {
    
        public int NrMesa { get; set; }
        public decimal ValorPago { get; set; }

        public bool Aberta { get; set; }              

        public AtendenteInput Atendente { get; set; }







    }

}
