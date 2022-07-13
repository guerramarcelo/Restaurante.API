using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurante.Classes
{
    public class Comanda
    {
        public Guid Id { get; private set; }      
        public Atendente Atendente { get; set; }
        public int NrMesa { get; private set; }
        public decimal ValorPago { get; private set; }
        public bool Aberta { get; private set; }
        public List<Pedido> Pedidos { get; private set; }
        public decimal Valor => ValorFinal();

        public Comanda(int nrMesa, Atendente atendente)
        {
            Id = Guid.NewGuid();
            this.NrMesa = nrMesa;
            this.Atendente = atendente;
            Pedidos = new List<Pedido>();
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
                Pedidos.Add(item);
            }
        }

        public void AdicionarPedido(Pedido pedido)
        {
            Pedidos.Add(pedido);
        }


        public void RemoverPedidos(Pedido pedido)
        {
            if (!Aberta)
            {
                Console.WriteLine("Não é possível remover pedidos de uma comanda fechada!");
                return;
            }

            Pedidos.Remove(pedido);
        }


        public decimal ValorTotal()
        {
            return Pedidos.Sum(pedido => pedido.ValorPedido());
        }

        public decimal ValorComissao()
        {
            return ValorTotal() * 0.1M;
        }

        public void SetComissao()
        {
            Atendente.PorcentagemComissao = ValorComissao();
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


        public override string ToString()
        {
            StringBuilder newString = new StringBuilder();
            newString.Append('-', 20);
            newString.AppendLine();

            foreach (var item in Pedidos)
            {
                foreach (var item2 in item.Produtos)
                {
                    newString.AppendFormat("{0} - {1} - {2}", item2.Produto.Nome, item2.Quantidade, item2.ValorItemPedido());
                    newString.AppendLine();
                }

            }
            newString.Append('-', 20);
            newString.AppendLine();
            newString.AppendLine("Valor: " + ValorFinal());
            newString.AppendLine("Valor Pago: " + ValorPago);

            return newString.ToString();
        }
    }
}


