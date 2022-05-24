using CadastroAtendente.Entidades;
using CadastroComanda.Entidades.Base;
using CadastroPedidos.Entidades;

namespace CadastroComanda.Entidades
{
    public class Comanda : Entidade
    {

        public Atendente atendente { get; private set; }
        public int NrMesa { get; private set; }
        public decimal ValorPago { get; private set; } 
        public bool Aberta { get; private set; }
        public List<Pedido> pedidos;

        public Comanda(int nrMesa, Atendente atendente)
        {
            this.NrMesa = nrMesa;
            this.atendente = atendente;
            pedidos = new List<Pedido>();
            ValorPago = 0.0M;
            Aberta = false;
        }


        public bool AbrirComanda()
        {
            Aberta = true;
            return Aberta;
        }


        public void AdicionarPedidos(List<Pedido> lista)
        {
            foreach (var item in lista)
            {
                pedidos.Add(item);

            }

        }


        public void RemoverPedidos(Pedido pedido)
        {
            if (!Aberta)
            {
                Console.WriteLine("Não é possível remover pedidos de uma comanda fechada!");
                return;
            }
            pedidos.Remove(pedido);
        }


        public decimal ValorTotal()
        {
            return pedidos.Sum(pedido => pedido.ValorPedido());
        }

        public decimal ValorComissao()
        {
            return ValorTotal() * 0.1M;
        }

        public void SetComissao()
        {
            atendente.PorcentagemComissao = ValorComissao();
        }


        public decimal ValorFinal()
        {
            return ValorTotal() + ValorComissao();
        }


        public void EfetuarPagamento(decimal valor)
        {
            ValorPago += valor;
            if (ValorPago > ValorFinal())
            {
                Console.WriteLine($"Troco: R${ValorPago - ValorFinal()}");
                return;
            }

            if (ValorPago < ValorFinal())
            {
                Console.WriteLine($"Valor restante: R${ValorFinal() - ValorPago}");
            }

        }

        public bool FecharComanda()
        {
            if (!Aberta)
            {
                Console.WriteLine("A comanda ja está fechada!");
                return Aberta;
            }

            if (ValorPago >= ValorFinal())
            {
                Console.WriteLine("Comanda fechada com sucesso!");
                return !Aberta;
            }

            else
            {
                Console.WriteLine($"Valor pago: R$ {ValorPago} é insuficiente para fechar a comanda!");
                return false;
            }
            Aberta = false;
            return Aberta;
        }

    }
}
