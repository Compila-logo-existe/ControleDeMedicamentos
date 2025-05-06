using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
{
    public IRepositorioPrescricaoMedica? IRepositorioPrescricaoMedica { get; set; }
    public TelaMedicamento TelaMedicamento { get; set; }
    private TelaPaciente TelaPaciente { get; set; }

    public TelaPrescricaoMedica
    (
        IRepositorioPrescricaoMedica repositorioPrescricaoMedicaEmArquivo,
        TelaMedicamento telaMedicamento,
        TelaPaciente telaPaciente

    ) : base
    (
        "Prescricao Medica",
        repositorioPrescricaoMedicaEmArquivo)
    {
        IRepositorioPrescricaoMedica = repositorioPrescricaoMedicaEmArquivo;
        TelaMedicamento = telaMedicamento;
        TelaPaciente = telaPaciente;
    }

    public override PrescricaoMedica ObterDados()
    {
        Console.WriteLine("Digite o CRM do Medico: ");
        string crmMedico = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Atribuindo data...");
        Thread.Sleep(1500);
        DateTime dataPrescricao = DateTime.Now;

        TelaPaciente.VisualizarRegistros(false);
        Console.Write("Digite o ID do Paciente: ");
        int idPaciente = int.Parse(Console.ReadLine()!);
        Paciente pacienteSelecionado = TelaPaciente.IRepositorioPaciente.SelecionarRegistroPorId(idPaciente);

        List<PrescricaoMedicamento> prescricoesMedicamentos = [];

        do
        {
            TelaMedicamento.VisualizarRegistros(false);

            int idMedicamento;

            while (true)
            {
                Console.Write("Digite o ID do Medicamento: ");
                bool idValido = int.TryParse(Console.ReadLine(), out idMedicamento);
                if (!idValido)
                {
                    Console.WriteLine("\nID inválido, escolha novamente.\n");
                    continue;
                }
                else
                    break;
            }

            Medicamento medicamento = TelaMedicamento.RepositorioMedicamento.SelecionarRegistroPorId(idMedicamento);

            Console.Write("Digite a Dosagem: ");
            string dosagem = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o Periodo: ");
            string periodo = Console.ReadLine() ?? string.Empty;

            int quantidadeMedicacao;

            while (true)
            {
                Console.Write("Digite a Quantidade: ");
                bool idValido = int.TryParse(Console.ReadLine(), out quantidadeMedicacao);
                if (!idValido)
                {
                    Console.WriteLine("\nQuantidade inválida, tente novamente.\n");
                    continue;
                }
                else
                    break;
            }

            PrescricaoMedicamento prescricaoMedicamento = new PrescricaoMedicamento(dosagem, periodo, medicamento, quantidadeMedicacao);

            prescricoesMedicamentos.Add(prescricaoMedicamento);

            Console.WriteLine("Deseja adicionar mais Prescricoes de Medicamentos? (S/n): ");
            string opcao = Console.ReadLine()!.ToUpper() ?? string.Empty;

            if (!string.IsNullOrEmpty(opcao))
            {
                if (opcao == "S")
                {
                    continue;
                }
                else if (opcao == "N")
                {
                    return new PrescricaoMedica(crmMedico, dataPrescricao, pacienteSelecionado, prescricoesMedicamentos);
                }
            }
        }
        while (true);
    }

    protected override void ExibirCabecalhoTabela()
    {
        IRepositorioPrescricaoMedica!.VerificarValidade();

        Console.WriteLine
        (
            "{0, -10} | {1, -10} | {2, -15} | {3, -20}",
            "ID", "CRMMedico", "Data Emissao", "Medicamentos"
        );
    }

    protected override void ExibirLinhaTabela(PrescricaoMedica pM)
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -10} | {2, -15} | {3, -20}",
            pM.Id, pM.CRMMedico, pM.Data?.ToString("dd/MM/yyyy"), pM.Medicamentos
        );
    }
}
