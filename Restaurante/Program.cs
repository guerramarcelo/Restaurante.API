using System;
using Restaurante.Classes;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Restaurante
{
    class Program
    {
        static void Main(string[] args)
        {

            //instanciei 3 produtos
            Produto produto1 = new Produto("Cerveja", 8.0M, Produto.TipoProdutoEnum.Bebida);
            Produto produto2 = new Produto("Pizza", 40.0M, Produto.TipoProdutoEnum.Comida);
            Produto produto3 = new Produto("Sorvete", 12.0M, Produto.TipoProdutoEnum.Sobremesa);

            ItemPedido itemPedido1 = new ItemPedido(produto1, 2);
            ItemPedido itemPedido2 = new ItemPedido(produto2, 1);
            ItemPedido itemPedido3 = new ItemPedido(produto3, 1);

            Pedido pedido1 = new Pedido();
            pedido1.AdicionarItemPedido(itemPedido3);
            pedido1.AdicionarItemPedido(itemPedido2);
            pedido1.AdicionarItemPedido(itemPedido1);

            Pedido pedido2 = new Pedido();
            pedido2.AdicionarItemPedido(itemPedido3);
            pedido2.AdicionarItemPedido(itemPedido2);

            List<Pedido> Pedidos = new List<Pedido>();
            {
                Pedidos.Add(pedido1);
                Pedidos.Add(pedido2);
            }

            Atendente atendente1 = new Atendente("Marcelo", "1234", 800.0M);

            Comanda comanda1 = new Comanda(1, atendente1);
            comanda1.AbrirComanda();
            comanda1.AdicionarPedidos(Pedidos);
            comanda1.EfetuarPagamento(1100.0M);
            comanda1.FecharComanda();            
            Console.WriteLine(comanda1);
         
            
            Arquivo arquivo = new Arquivo();
      
            string caminho = ("D:\\Arquivos\\arquivo-teste.txt");     
            arquivo.EscreverArquivoStream(caminho, comanda1.ToString());
              
            
        }
          

        public static void Escrever(string nomeProduto, decimal valorItemPedido)
        {
            Console.WriteLine(nomeProduto + " - " + valorItemPedido);
        }


    }
}
