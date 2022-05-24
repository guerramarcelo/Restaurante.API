using System;
using System.Collections.Generic;

namespace Restaurante.Classes
{
    public class Atendente : Funcionario
    {
        public Atendente(string nome, string cpf, decimal salario)
        {
            Nome = nome;
            Cpf = cpf;
            Salario = salario;
            
        }



        public Atendente()
        {
            Nome = String.Empty;
            Cpf = String.Empty;
            Salario = 0.0M;

        }


        public int Id { get; set; }
         
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public decimal Salario { get; set; }
        public decimal PorcentagemComissao { get; set; }









    }
   
  
}



//Atendente atendente1 = new Atendente("Marcelo", "1234", 100.0M);
//Atendente atendente2 = new Atendente("Bruno", "4321", 200.0M);
//Atendente atendente3 = new Atendente("Felipe", "2468", 300.0M);