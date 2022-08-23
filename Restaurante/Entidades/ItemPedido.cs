using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Restaurante.Classes
{
    public class ItemPedido 
    {
        public Guid Id { get; private set; }

        public ItemPedido( Produto produto,  int quantidade = 1 )
        {
            Id = Guid.NewGuid();
            Quantidade = quantidade;
            Produto = produto;            
        }              
        public ItemPedido()
        {
            Id= Guid.NewGuid();
        }

        public Produto Produto { get;  set; }       
        public int Quantidade { get;  set; }
        
        public decimal ValorItemPedido()
        {
            var valorItemPedido = Produto.Preco * Quantidade;
            return valorItemPedido;
        }

    }
}

