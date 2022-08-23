using System;
using System.Collections.Generic;

namespace Restaurante.Classes
{
    public class Atendente 
    {
        public Atendente(string nome, string cpf, decimal salario, decimal porcentagemComissao)
        {
            Nome = nome;
            Cpf = cpf;
            Salario = salario; 
            PorcentagemComissao = porcentagemComissao;
            Id = Guid.NewGuid();
        }

        public Atendente(Guid id, string nome, string cpf, decimal salario, decimal porcentagemComissao)
        {
            Nome = nome;
            Cpf = cpf;
            Salario = salario;
            PorcentagemComissao = porcentagemComissao;
            Id = id;
        }
        public Atendente ()
        {
            Id = Guid.NewGuid();
        }

                
        public Guid Id { get; private set; }
        public decimal PorcentagemComissao { get; set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; } 
        public decimal Salario { get; private set; }

    }  
}
