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
        public void ListarDiretorios(string caminho)
        {
            var retornoCaminho = Directory.GetDirectories(caminho);

            foreach (var retorno in retornoCaminho)
            {
                Console.WriteLine(retorno);
            }

        }

        

        public void CriarArquivoStream(string caminho, object conteudo)
        {
            if (!File.Exists(caminho))
            {
                
                var temp = conteudo.ToString();
                List<string> linhas = new List<string>();

                foreach (var linha in temp)
                {
                    Console.Write(linha);
                }
                linhas.Add(temp);

                using (var stream = File.CreateText(caminho))
                {
                    foreach (var linha in temp)
                    {
                        stream.Write(linha);
                    }
                }

            }
        }


  

    }

}