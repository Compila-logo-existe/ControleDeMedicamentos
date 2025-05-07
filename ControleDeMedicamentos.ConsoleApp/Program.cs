using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;
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

            bool menuSelecionado = true;
            while (menuSelecionado)
            {
                char opcaoEscolhida = telaSelecionada.ApresentarMenu();

                if (telaSelecionada is TelaFornecedor)
                {
                    TelaFornecedor telaFornecedor = (TelaFornecedor)telaSelecionada;

                    if (opcaoEscolhida == '5')
                    {
                        telaFornecedor.VisualizarMedicamentosFornecedor(); continue;
                    }
                }

                else if (telaSelecionada is TelaPrescricaoMedica)
                {
                    TelaPrescricaoMedica telaPrescricaoMedica = (TelaPrescricaoMedica)telaSelecionada;

                    switch (opcaoEscolhida)
                    {
                        case '1': telaPrescricaoMedica.CadastrarRegistro(); continue;

                        case '2': telaPrescricaoMedica.GerarRelatorio(true); continue;

                        case 'S': menuSelecionado = false; break;

                        default:
                            Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red); break;
                    }
                }

                else if (telaSelecionada is TelaRequisicaoEntrada)
                {
                    TelaRequisicaoEntrada telaRequisicaoEntrada = (TelaRequisicaoEntrada)telaSelecionada;

                    switch (opcaoEscolhida)
                    {
                        case '1': telaRequisicaoEntrada.CadastrarRegistro(); continue;

                        case '2': telaRequisicaoEntrada.VisualizarRegistros(true); continue;

                        case 'S': menuSelecionado = false; break;

                        default:
                            Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red); break;
                    }
                }

                else if (telaSelecionada is TelaRequisicaoSaida)
                {
                    TelaRequisicaoSaida telaRequisicaoSaida = (TelaRequisicaoSaida)telaSelecionada;

                    switch (opcaoEscolhida)
                    {
                        case '1': telaRequisicaoSaida.CadastrarRegistro(); continue;

                        case '2': telaRequisicaoSaida.VisualizarRegistros(true); continue;

                        case '3': telaRequisicaoSaida.VisualizarRequisicoesPaciente(); continue;

                        case 'S': menuSelecionado = false; break;

                        default:
                            Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red); break;
                    }
                }

            switch (opcaoEscolhida)
            {
                case '1': telaSelecionada.CadastrarRegistro(); break;

                    case '2': telaSelecionada.EditarRegistro(); break;

                    case '3': telaSelecionada.ExcluirRegistro(); break;

                    case '4': telaSelecionada.VisualizarRegistros(true); break;

                    case 'S': menuSelecionado = false; break;

                    default:
                        Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red); break;
                }
            }
        }
    }
}
