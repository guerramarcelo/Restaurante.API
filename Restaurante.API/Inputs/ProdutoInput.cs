using Restaurante.Classes;
using static Restaurante.Classes.Produto;

namespace Restaurante.API.Inputs
{
    public class ProdutoInput
    {

        public string Nome { get;  set; }

        public decimal Preco { get; set; }

        public TipoProdutoEnum Tipo { get; set; }

        public Guid Id { get; set; }


        




    }
}
