using System;

namespace Restaurante.Classes
{
    public abstract class Funcionario
    {
        protected Guid Id { get; set; }
        protected string Nome { get; set; }
        protected string Cpf { get; set; }
        protected decimal Salario { get; set; } 
        
    }
}