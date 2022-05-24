using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Classes
{
    public class Pedido
    {

        public Pedido()
        {
            Id = Guid.NewGuid();
            HoraPedido = DateTime.Now;
            Produtos = new List<ItemPedido>();
        }



        public Guid Id { get; private set; }

        public List<ItemPedido> Produtos { get; private set; }


        public DateTime HoraPedido { get; private set; }


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
