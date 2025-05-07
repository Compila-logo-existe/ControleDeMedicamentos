using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.White;
        TelaPrincipal telaPrincipal = new TelaPrincipal();

        while (true)
        {
            telaPrincipal.ApresentarMenuPrincipal();

            ITelaCrud telaSelecionada = telaPrincipal.ObterTela();

            if (telaPrincipal.opcaoPrincipal != null && telaPrincipal.opcaoPrincipal.ToUpper() == "S")
            {
                Console.Clear();
                Notificador.ExibirMensagem("Adeus (T_T)/\n\n", ConsoleColor.Blue);

                return;
            }

            if (telaSelecionada == null)
            {
                Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red);
                Notificador.PressioneEnter("CONTINUAR");

                continue;
            }

            bool menuSelecionado = true;
            while (menuSelecionado)
            {
                string opcaoEscolhida = telaSelecionada.ApresentarMenu();

                if (telaSelecionada is TelaFornecedor)
                {
                    TelaFornecedor telaFornecedor = (TelaFornecedor)telaSelecionada;

                    if (opcaoEscolhida == "5")
                    {
                        telaFornecedor.VisualizarMedicamentosFornecedor();
                        Notificador.PressioneEnter("CONTINUAR"); continue;
                    }
                }

                else if (telaSelecionada is TelaMedicamento)
                {
                    TelaMedicamento telaMedicamento = (TelaMedicamento)telaSelecionada;

                    if (opcaoEscolhida == "5")
                    {
                        telaMedicamento.ExtrairParaCSV();
                        Notificador.PressioneEnter("CONTINUAR"); continue;
                    }
                }

                else if (telaSelecionada is TelaPrescricaoMedica)
                {
                    TelaPrescricaoMedica telaPrescricaoMedica = (TelaPrescricaoMedica)telaSelecionada;

                    switch (opcaoEscolhida)
                    {
                        case "1":
                            telaPrescricaoMedica.CadastrarRegistro();
                            Notificador.PressioneEnter("CONTINUAR"); continue;

                        case "2":
                            telaPrescricaoMedica.GerarRelatorio(true);
                            Notificador.PressioneEnter("CONTINUAR"); continue;

                        case "S": menuSelecionado = false; continue;

                        default:
                            Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red); break;
                    }

                    Notificador.PressioneEnter("CONTINUAR"); continue;
                }

                else if (telaSelecionada is TelaRequisicaoEntrada)
                {
                    TelaRequisicaoEntrada telaRequisicaoEntrada = (TelaRequisicaoEntrada)telaSelecionada;

                    switch (opcaoEscolhida)
                    {
                        case "1":
                            telaRequisicaoEntrada.CadastrarRegistro();
                            Notificador.PressioneEnter("CONTINUAR"); continue;

                        case "2":
                            telaRequisicaoEntrada.VisualizarRegistros(true);
                            Notificador.PressioneEnter("CONTINUAR"); continue;

                        case "S": menuSelecionado = false; continue;

                        default:
                            Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red); break;
                    }
                }

                else if (telaSelecionada is TelaRequisicaoSaida)
                {
                    TelaRequisicaoSaida telaRequisicaoSaida = (TelaRequisicaoSaida)telaSelecionada;

                    switch (opcaoEscolhida)
                    {
                        case "1":
                            telaRequisicaoSaida.CadastrarRegistro();
                            Notificador.PressioneEnter("CONTINUAR"); continue;

                        case "2":
                            telaRequisicaoSaida.VisualizarRegistros(true);
                            Notificador.PressioneEnter("CONTINUAR"); continue;

                        case "3":
                            telaRequisicaoSaida.VisualizarRequisicoesPaciente();
                            Notificador.PressioneEnter("CONTINUAR"); continue;

                        case "S": menuSelecionado = false; continue;

                        default:
                            Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red); break;
                    }
                }

                switch (opcaoEscolhida)
                {
                    case "1": telaSelecionada.CadastrarRegistro(); break;

                    case "2": telaSelecionada.EditarRegistro(); break;

                    case "3": telaSelecionada.ExcluirRegistro(); break;

                    case "4": telaSelecionada.VisualizarRegistros(true); break;

                    case "S": menuSelecionado = false; continue;

                    default:
                        Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red); break;
                }

                Notificador.PressioneEnter("CONTINUAR");
            }
        }
    }
}
