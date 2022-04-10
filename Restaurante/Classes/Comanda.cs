using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurante.Classes
{
    public class Comanda
    {
        private Guid id;
        private Atendente atendente;
        private int nrMesa;
        private decimal valorPago;
        private bool aberta;
        private List<Pedido> pedidos;


        public Comanda(int nrMesa, Atendente atendente)
        {
            id = Guid.NewGuid();
            this.nrMesa = nrMesa;
            this.atendente = atendente;
            pedidos = new List<Pedido>();
            valorPago = 0.0M;
            aberta = false;

        }

        public bool AbrirComanda()
        {
            return aberta = true;
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
            if (!AbrirComanda())
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

            if (valor > ValorFinal())
            {
                Console.WriteLine($"Troco: R${valor - ValorFinal()}");
            }

            if (valor < ValorFinal())
            {
                Console.WriteLine($"Valor restante: R${ValorFinal() - valor}");
            }

            valorPago += valor;

        }

        public bool FecharComanda()
        {
            if (!AbrirComanda())
            {
                Console.WriteLine("A comanda ja está fechada!");
                return AbrirComanda();
            }

            if (valorPago >= ValorFinal())
            {
                Console.WriteLine("Comanda fechada com sucesso!");
                return (!AbrirComanda());
            }

            else
                Console.WriteLine($"Valor pago: R$ {valorPago} é insuficiente para fechar a comanda!");
            return !aberta;
        }

        public void ImprimirComanda()
        {
            foreach (var item in pedidos)
            {
                foreach (var item2 in item.Produtos)
                {
                    Console.WriteLine($"{item2.Produto.Nome} - {item2.Quantidade} - {item2.ValorItemPedido()}");
                }
            }

            Console.WriteLine($"Valor: {ValorFinal()}");
            Console.WriteLine($"Valor Pago: {valorPago}");


        }

    }

}


