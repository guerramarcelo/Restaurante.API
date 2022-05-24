using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurante.Classes
{
    public class Comanda
    {
        public Guid Id { get; private set; }
        public Atendente atendente { get; set; }
        public int nrMesa { get; private set; }
        public decimal ValorPago { get; private set; }
        public bool aberta { get; private set; }
        private List<Pedido> pedidos;



        public Comanda(int nrMesa, Atendente atendente)
        {
            Id = Guid.NewGuid();
            this.nrMesa = nrMesa;
            this.atendente = atendente;
            pedidos = new List<Pedido>();
            ValorPago = 0.0M;
            aberta = false;

        }




        public bool AbrirComanda()
        {
            aberta = true;
            return aberta;
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
            if (!aberta)
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
            if (!aberta)
            {
                Console.WriteLine("A comanda ja está fechada!");
                return aberta;
            }

            if (ValorPago >= ValorFinal())
            {
                Console.WriteLine("Comanda fechada com sucesso!");
                return !aberta;
            }

            else
            {
                Console.WriteLine($"Valor pago: R$ {ValorPago} é insuficiente para fechar a comanda!");
                return false;
            }
            aberta = false;
            return aberta;
        }

        //public void ImprimirComanda()
        //{
        //    Console.WriteLine("----------------------");
        //    foreach (var item in pedidos)
        //    {
        //        foreach (var item2 in item.Produtos)
        //        {
        //            Console.WriteLine($"{item2.Produto.Nome} - {item2.Quantidade} - {item2.ValorItemPedido()}");
        //        }
        //    }

        //    Console.WriteLine($"Valor: {ValorFinal()}");
        //    Console.WriteLine($"Valor Pago: {valorPago}");


        //}

        public override string ToString()
        {
            StringBuilder newString = new StringBuilder();

            newString.Append('-', 20);
            newString.AppendLine();

            foreach (var item in pedidos)
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


