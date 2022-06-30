namespace Restaurante.API.Inputs
{
    public class ComandaInput
    {
        public Guid Id { get; set; }
        public int NrMesa { get; set; }
        public decimal ValorPago { get; set; }
        public bool Aberta { get; set; }
        public Guid AtendenteId { get; set; }
    }
}
