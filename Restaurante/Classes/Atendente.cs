using System;

namespace Restaurante.Classes
{
    public class Atendente : Funcionario
    {
        
        public decimal PorcentagemComissao { get; set; }
        public Atendente (string nome, string cpf, decimal salario)
        {
            id = Guid.NewGuid();
            
        }
            
        public void ReceberPedidos()
        {
            
        }

        
    }
}