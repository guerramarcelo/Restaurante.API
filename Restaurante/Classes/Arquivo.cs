using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Classes
{
    public class Arquivo
    {
        public void CriarArquivo(string caminho, string conteudo)
        {
            File.WriteAllText(caminho, conteudo);
        }

        public void ListarDiretorios(string caminho)
        {
            var retornoCaminho = Directory.GetDirectories(caminho);

            foreach (var retorno in retornoCaminho)
            {
                Console.WriteLine(retorno);
            }

        }

        public void EscreverArquivoStream(string caminho, string conteudo)
        {
            List<string> linhas = new List<string>();
            linhas = File.ReadAllLines(caminho).ToList();

            foreach (var linha in conteudo)
            {
                Console.WriteLine(linha);
            }

            linhas.Add(conteudo.ToString());
            File.WriteAllLines(caminho, linhas);


        }





    }

}
