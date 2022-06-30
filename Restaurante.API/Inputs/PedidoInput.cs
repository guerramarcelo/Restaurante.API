namespace Restaurante.API.Inputs
{
    public class PedidoInput
    {
        public Guid Id { get; set; }
        public Guid ComandaId { get; set; }
        public DateTime Hora { get; set; }        
    }


    public class ItemPedidoInput
    {   
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public Guid PedidoId { get; set; }          
    }




}


