using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        TelaPrincipal telaPrincipal = new TelaPrincipal();

        while (true)
        {
            telaPrincipal.ApresentarMenuPrincipal();

            ITelaCrud telaSelecionada = telaPrincipal.ObterTela();

            if (telaSelecionada == null)
            {
                Console.WriteLine("\nOpção Inválida!");
                Console.Write("Pressione [Enter] para tentar novamente.");
                Console.ReadKey();
                continue;
            }

            char opcaoEscolhida = telaSelecionada.ApresentarMenu();

            if (telaSelecionada is TelaRequisicaoSaida)
            {
                TelaRequisicaoSaida telaRequisicaoSaida = (TelaRequisicaoSaida)telaSelecionada;

                if (opcaoEscolhida == '3')
                {
                    telaRequisicaoSaida.VisualizarRequisicoesDePaciente(); continue;
                }
            }

            switch (opcaoEscolhida)
            {
                case '1': telaSelecionada.CadastrarRegistro(); break;

                case '2': telaSelecionada.EditarRegistro(); break;

                case '3': telaSelecionada.ExcluirRegistro(); break;

                case '4': telaSelecionada.VisualizarRegistros(true); break;

                case 'S': break;

                default:
                    Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red); break;
            }
        }
    }
}
