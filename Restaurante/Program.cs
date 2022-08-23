using System;
using Restaurante.Classes;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Restaurante
{
    class Program
    {        
        static void Main(string[] args)
        {           

            //instanciei 3 produtos
            Produto produto1 = new("Cerveja", 8.0M, Produto.TipoProdutoEnum.Bebida);
            Produto produto2 = new("Pizza", 40.0M, Produto.TipoProdutoEnum.Comida);
            Produto produto3 = new("Sorvete", 12.0M, Produto.TipoProdutoEnum.Sobremesa);

            ItemPedido itemPedido1 = new(produto1, 2);
            ItemPedido itemPedido2 = new(produto2, 1);
            ItemPedido itemPedido3 = new(produto3, 1);

            Pedido pedido1 = new();
            pedido1.AdicionarItemPedido(itemPedido3);
            pedido1.AdicionarItemPedido(itemPedido2);
            pedido1.AdicionarItemPedido(itemPedido1);

            Pedido pedido2 = new();
            pedido2.AdicionarItemPedido(itemPedido3);
            pedido2.AdicionarItemPedido(itemPedido2);

            List<Pedido> Pedidos = new();
            {
                Pedidos.Add(pedido1);
                Pedidos.Add(pedido2);
            }

            Atendente atendente1 = new("Marcelo", "1234", 800.0M, 10);

            Comanda comanda1 = new(1, atendente1);
            comanda1.AbrirComanda();
            comanda1.AdicionarPedidos(Pedidos);
            comanda1.EfetuarPagamento(132.0M);
            comanda1.FecharComanda();
            Console.WriteLine(comanda1);


            //Console.WriteLine("------------");
            //Console.WriteLine("Digite o nome do arquivo que deseja salvar");
            //string nome = Console.ReadLine() + ".txt";
            //string path = "D:\\Arquivos";
            //string caminho = Path.Combine(path, nome);
            //Arquivo arquivo = new Arquivo();            
            //arquivo.CriarArquivoStream(caminho, comanda1);

        }

        public static void Escrever(string nomeProduto, decimal valorItemPedido)
        {
            Console.WriteLine(nomeProduto + " - " + valorItemPedido);
        }

    }
}
