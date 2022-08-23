using Restaurante.Classes;

namespace Restaurante.API.Inputs
{
    public class PedidoInput
    {
        public Guid Id { get; set; }
        public DateTime Hora { get; set; } 
        public ItemPedido Produto { get; set; }
        public Guid ComandaId { get; set; }
    }


    public class ItemPedidoInput
    {   
        public Guid Id { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }              
    }

}


