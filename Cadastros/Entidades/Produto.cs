using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurante.Classes.Produto;

namespace CadastroProdutos.Entidades
{       

    public class Produto : Entidade
    {       

        public Produto(decimal preco, TipoProdutoEnum tipo, string nome)
        {
            Preco = preco;
            Tipo = tipo;
            Nome = nome;
        }

        public decimal Preco { get; private set; }
        public TipoProdutoEnum Tipo { get; private set; }
        public string Nome { get; private set; }





    }


    


}









//public Produto(string nome, decimal preco, TipoProdutoEnum tipo)
//{
//    Id = Guid.NewGuid();
//    Preco = preco;
//    Nome = nome;
//    Tipo = tipo;

//}
