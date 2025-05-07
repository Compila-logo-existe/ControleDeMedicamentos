using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;
using ControleDeMedicamentos.ConsoleApp.Util;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

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

            if (telaSelecionada is TelaFornecedor)
            {
                TelaFornecedor telaFornecedor = (TelaFornecedor)telaSelecionada;

                if (opcaoEscolhida == '5')
                {
                    telaFornecedor.VisualizarMedicamentosFornecedor(); continue;
                }
            }

            if (telaSelecionada is TelaRequisicaoEntrada)
            {
                TelaRequisicaoEntrada telaRequisicaoEntrada = (TelaRequisicaoEntrada)telaSelecionada;

                if (opcaoEscolhida == '1')
                {
                    telaRequisicaoEntrada.CadastrarRegistro(); continue;
                }
                if (opcaoEscolhida == '2')
                {
                    telaRequisicaoEntrada.VisualizarRegistros(true); continue;
                }
            }

            if (telaSelecionada is TelaRequisicaoSaida)
            {
                TelaRequisicaoSaida telaRequisicaoSaida = (TelaRequisicaoSaida)telaSelecionada;

                if (opcaoEscolhida == '1')
                {
                    telaRequisicaoSaida.CadastrarRegistro(); continue;
                }
                if (opcaoEscolhida == '2')
                {
                    telaRequisicaoSaida.VisualizarRegistros(true); continue;
                }
                if (opcaoEscolhida == '3')
                {
                    telaRequisicaoSaida.VisualizarRequisicoesPaciente(); continue;
                }
            }

            if (telaSelecionada is TelaPrescricaoMedica)
            {
                TelaPrescricaoMedica telaPrescricaoMedica = (TelaPrescricaoMedica)telaSelecionada; 

                if (opcaoEscolhida == '1')
                {
                    telaPrescricaoMedica.CadastrarRegistro();    
                }
                else if (opcaoEscolhida == '2')
                {
                    telaPrescricaoMedica.GerarRelatorio(false);
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
