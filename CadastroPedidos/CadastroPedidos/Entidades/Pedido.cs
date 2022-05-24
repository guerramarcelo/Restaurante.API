using CadastroItemPedido;
using CadastroPedidos.Entidades.Base;

namespace CadastroPedidos.Entidades
{
    public class Pedido : Entidade
    {
        public Pedido()
        {
            HoraPedido = DateTime.Now;
            Produtos = new List<ItemPedido>();
        }

        public DateTime HoraPedido { get; private set; }
        
        public List<ItemPedido> Produtos { get; private set; }

        public void AdicionarItemPedido(ItemPedido itemPedido)
        {
            Produtos.Add(itemPedido);
        }


        public void RemoverItemPedido(ItemPedido itemPedido)
        {
            Produtos.Remove(itemPedido);
        }


        public decimal ValorPedido()
        {
            decimal total = 0.0M;
            foreach (var itemPedido in Produtos)
            {
                total = total + itemPedido.ValorItemPedido();
            }
            return total;

        }


    }
}
