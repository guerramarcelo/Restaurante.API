using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public Produto (Guid id, string nome, decimal preco, TipoProdutoEnum tipo)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Tipo = tipo;
        }
        public Produto()
        {

        }


        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public TipoProdutoEnum Tipo { get; private set; }
        public enum TipoProdutoEnum
        {
            Bebida,
            Comida,
            Sobremesa, 
        }
        
        
    }
}
