using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Classes
{
    public class Produto
    {

        public Guid Id { get; private set; }

        public Produto(string nome, decimal preco, TipoProdutoEnum tipo)
        {
            Id = Guid.NewGuid();
            Preco = preco;
            Nome = nome;
            Tipo = tipo;

        }


        public string Nome { get; private set; }

        public decimal Preco { get; private set; }


        public enum TipoProdutoEnum
        {

            Bebida,
            Comida,
            Sobremesa

        }

        public TipoProdutoEnum Tipo { get; private set; }



    }


}
