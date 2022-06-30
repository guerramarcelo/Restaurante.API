using System.Data.SqlClient;

namespace Restaurante.API.Inputs
{
    public class AtendenteInput
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public decimal Salario { get; set; }
        public decimal PorcentagemComissao { get; set; }

    }
}
