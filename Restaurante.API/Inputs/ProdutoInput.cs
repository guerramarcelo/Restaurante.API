using Dapper;
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


       //public static int InsertProduto(string nome, decimal preco,TipoProdutoEnum tipo)
       // {
       //     ProdutoInput produto = new()
       //     {
       //         Nome = nome,
       //         Preco = preco,
       //         Tipo = tipo

       //     };
       //     string sql = @"insert into dbo.Produto (Nome, Preco, Tipo)
       //         values (@Nome, @Preco, @Tipo);";

       //     return SqlDataAccess.SaveData(sql, produto);

       // }

       // public static List<Produto> LoadProdutos()
       // {
       //     string sql = @"select Id, Nome, Preco, Tipo from dbo.Produto;";
       //     return SqlDataAccess.LoadData<Produto>(sql);
       // }



    }
}
