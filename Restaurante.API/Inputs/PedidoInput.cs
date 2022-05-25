namespace Restaurante.API.Inputs
{
    public class PedidoInput
    {
        public List<ItemPedidoInput> Items { get; set; }
    }
    public class ItemPedidoInput
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
