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
            Id = Guid.NewGuid();
        }
        public Atendente()
        {
            Nome = String.Empty;
            Cpf = String.Empty;
            Salario = 0.0M;
        }
        public Guid Id { get; private set; }         
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public decimal Salario { get; private set; }
        public decimal PorcentagemComissao { get;set; }
    }  
}
