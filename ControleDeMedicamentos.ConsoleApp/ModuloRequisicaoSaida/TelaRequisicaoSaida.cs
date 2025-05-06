using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaCrud
{
    private TelaPaciente TelaPaciente;
    private TelaPrescricaoMedica TelaPrescricaoMedica;
    private IRepositorioRequisicaoSaida RepositorioRequisicaoSaida;

    public TelaRequisicaoSaida(IRepositorioRequisicaoSaida repositorioRequisicaoSaida, TelaPaciente telaPaciente, TelaPrescricaoMedica telaPrescricaoMedica) : base("Requisição de Saída", repositorioRequisicaoSaida)
    {
        TelaPaciente = telaPaciente;
        TelaPrescricaoMedica = telaPrescricaoMedica;
        RepositorioRequisicaoSaida = repositorioRequisicaoSaida;
    }

    public override char ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"1 - Cadastrar Requisição de Saída");
        Console.WriteLine($"2 - Visualizar Requisições de Saídas");
        Console.WriteLine($"3 - Visualizar Requisição de Saída de um Paciente");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        char operacaoEscolhida = Convert.ToChar(Console.ReadLine()!);

        return operacaoEscolhida;
    }
    public override RequisicaoSaida ObterDados()
    {
        string data;

        Console.Write("A data de entrada é hoje? (S/n) ");
        string opcao = Console.ReadLine() ?? string.Empty;

        while (true)
        {
            if (!string.IsNullOrEmpty(opcao) && opcao.ToUpper() == "S")
            {
                data = default!;
                break;
            }
            if (!string.IsNullOrWhiteSpace(opcao) && opcao.ToLower() == "n")
            {
                Console.Write("Digite uma data de entrada: (dd/MM/yyyy)");
                data = Console.ReadLine()!;
                break;
            }
            else
            {
                Console.WriteLine("\nOpção inválida!\n");
                continue;
            }
        }

        TelaPaciente.VisualizarRegistros(false);

        int idPacienteEscolhido;

        while (true)
        {
            Console.Write("Digite o ID de um paciente para registrar a saída: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idPacienteEscolhido);
            if (!idValido)
            {
                Console.WriteLine("\nID inválido, selecione novamente.\n");
                continue;
            }
            else
                break;
        }

        // tem algo de acessibilidade aq p arruma
        Paciente paciente = TelaPaciente.IRepositorioPaciente.SelecionarRegistroPorId(idPacienteEscolhido);

        TelaPrescricaoMedica.VisualizarRegistros(false);

        int idPrescricaoMedicaEscolhida;

        while (true)
        {
            Console.Write("Digite o ID de uma prescrição médica para registrar a saída: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idPrescricaoMedicaEscolhida);
            if (!idValido)
            {
                Console.WriteLine("\nID inválido, selecione novamente.\n");
                continue;
            }
            else
                break;
        }

        PrescricaoMedica prescricaoMedica = TelaPrescricaoMedica.IRepositorioPrescricaoMedica.SelecionarRegistroPorId(idPrescricaoMedicaEscolhida);

        List<Medicamento> medicamentosRequisitados = PrescricaoMedica.Medicamentos;

        RequisicaoSaida requisicaoSaida = new RequisicaoSaida(data, paciente, prescricaoMedica, medicamentosRequisitados);

        return requisicaoSaida;
    }

    public void VisualizarRequisicoesDePaciente()
    {
        //
    }

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -20} | {3, -10} | {4, -15}",
                "ID", "Data", "Paciente", "ID Prescrição Médica", "Qtd de Medicamentos Requisitados");
    }

    protected override void ExibirLinhaTabela(RequisicaoSaida registro)
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -15}",
                registro.Id, registro.Data, registro.Paciente.Nome, registro.PrescicaoMedica.Id, registro.MedicamentosRequisitados.Count.ToString());
    }
}
