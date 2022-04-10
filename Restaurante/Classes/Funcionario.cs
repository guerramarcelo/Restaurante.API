using System;

namespace Restaurante.Classes
{
    public abstract class Funcionario
    {
        protected Guid id;
        protected string nome;
        protected string cpf;
        protected decimal salario; 


        public Funcionario()
        {
            
        }

    
    }
}