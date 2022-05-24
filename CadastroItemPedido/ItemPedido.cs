using CadastroItemPedido.Entidades;
using CadastroProdutos.Entidades;

namespace CadastroItemPedido
{
    public class ItemPedido 
    {
        public ItemPedido(Produto produto, int quantidade = 1)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public Produto Produto { get; private set; }
        public int Quantidade { get; private set; }


        public decimal ValorItemPedido()
        {
            var valorItemPedido = Produto.Preco * Quantidade;
            return valorItemPedido;
        }




    }
}
