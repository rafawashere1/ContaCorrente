namespace ContaCorrente.ConsoleApp
{
    internal class ContaCorrente
    {

        public Movimentacao[] movimentacoes;
        public decimal Saldo { get; set; }
        public int Numero { get; set; }
        public decimal Limite { get; set; }
        public bool EhEspecial { get; set; }

        private int indice = 0;

        public decimal Sacar(decimal valor)
        {
            if (valor >= Limite + Saldo)
            {
                Console.WriteLine(">> Limite de saque excedido.\n");
            }

            else
            {
                Saldo -= valor;

                Console.WriteLine($">> Saque no valor de {valor:F2} efetuado com sucesso!\n");

                ExibirSaldo();

                AdicionarMovimentacao("DEBITO", $">> Saque no valor de {valor:F2}\n");

                Console.ReadKey();
            }

            return Saldo;
        }

        public decimal Depositar(decimal valor)
        {
            Saldo += valor;

            Console.WriteLine($">> Depósito no valor de {valor:F2} efetuado com sucesso!\n");

            ExibirSaldo();

            AdicionarMovimentacao("CREDITO", $">> Depósito no valor de {valor:F2}\n");

            Console.ReadKey();

            return Saldo;
        }

        public void TransferirPara(ContaCorrente conta, decimal valor)
        {
            if (conta == this)
            {
                Program.ColorirMensagem($"Não é possível transferir para sua própria conta!", "QUEBRAR-LINHA", ConsoleColor.Red);
            }

            else if (Saldo < valor)
            {
                Program.ColorirMensagem($"Saldo insuficiente!", "QUEBRAR-LINHA", ConsoleColor.Red);
            }

            else
            {
                Saldo -= valor;

                conta.Saldo += valor;

                Console.WriteLine($">> Transferencia no valor de {valor:F2} para a conta de número {conta.Numero} enviada com sucesso!\n");

                ExibirSaldo();

                AdicionarMovimentacao("DEBITO", $">> Transferencia no valor de {valor:F2} para a conta de número {conta.Numero}\n");
            }      
        }

        public void AdicionarMovimentacao(string tipoDeMovimentacao, string mensagem)
        {
            Movimentacao movimentacao = new();
            movimentacao.Tipo = tipoDeMovimentacao;
            movimentacao.Mensagem = mensagem;

            for (int i = 0; i < movimentacoes.Length; i++)
            {
                movimentacoes[indice] = movimentacao;
            }

            indice++;

        }

        public void ExibirExtrato()
        {
            Program.ColorirMensagem("-------------------------- Extrato --------------------------\n", "QUEBRAR-LINHA", ConsoleColor.Yellow);

            for (int i = 0; i < indice; i++)
            {
                if (movimentacoes[i].Tipo == "DEBITO")
                {
                    Program.ColorirMensagem(movimentacoes[i].Mensagem, "QUEBRAR-LINHA", ConsoleColor.Red);
                }

                else if (movimentacoes[i].Tipo == "CREDITO")
                {
                    Program.ColorirMensagem(movimentacoes[i].Mensagem, "QUEBRAR-LINHA", ConsoleColor.Green);
                }
            }

            Program.ColorirMensagem("-------------------------- Extrato --------------------------\n", "QUEBRAR-LINHA", ConsoleColor.Yellow);
        }

        public void ExibirSaldo()
        {
            Console.WriteLine($">> Seu saldo é: {Saldo:F2}\n");
        }
    }
}
