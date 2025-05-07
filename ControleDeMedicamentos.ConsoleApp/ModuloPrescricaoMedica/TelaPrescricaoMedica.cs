using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.Util;

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

    public override string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"1 - Cadastrar Prescricao Medica");
        Console.WriteLine($"2 - Gerar Relatorios de Prescricao Medica");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }

    public override PrescricaoMedica ObterDados()
    {
        Console.Write("Digite o CRM do Medico: ");
        string crmMedico = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Atribuindo data...");
        Thread.Sleep(1500);
        DateTime dataPrescricao = DateTime.Now.Date;

        TelaPaciente.VisualizarRegistros(false);

        int idPacienteEscolhido;

        while (true)
        {
            Console.Write("\nDigite o ID do Paciente: ");
            bool qtdEstoque = int.TryParse(Console.ReadLine(), out idPacienteEscolhido);

            if (!qtdEstoque)
            {
                Notificador.ExibirMensagem("\nID inválido, selecione novamente.\n", ConsoleColor.Red);

                continue;
            }
            else
                break;
        }

        Paciente pacienteSelecionado = TelaPaciente.IRepositorioPaciente.SelecionarRegistroPorId(idPacienteEscolhido);

        List<PrescricaoMedicamento> prescricoesMedicamentos = [];

        do
        {
            TelaMedicamento.VisualizarRegistros(false);

            int idMedicamento;

            while (true)
            {
                Console.Write("\nDigite o ID do Medicamento: ");
                bool idValido = int.TryParse(Console.ReadLine(), out idMedicamento);

                if (!idValido)
                {
                    Console.WriteLine("\nID inválido, selecione novamente.\n");

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

            PrescricaoMedicamento prescricaoMedicamento = new(dosagem, periodo, medicamento, quantidadeMedicacao);

            prescricoesMedicamentos.Add(prescricaoMedicamento);

            while (true)
            {
                Console.Write("Deseja adicionar mais Prescricoes de Medicamentos? (S/n): ");
                string opcao = Console.ReadLine()! ?? string.Empty;

                if (!string.IsNullOrEmpty(opcao))
                {
                    opcao = opcao.ToUpper();

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
        }
        while (true);
    }

    public void GerarRelatorio(bool exibirTitulo)
    {
        if (exibirTitulo)
            ExibirCabecalho();

        List<PrescricaoMedica> prescricoesMedicas = IRepositorioPrescricaoMedica!.PegarRegistros();

        foreach (PrescricaoMedica registro in prescricoesMedicas)
        {
            VisualizarRegistros(false);

            int idRelatorioEscolhido;

            while (true)
            {
                Console.Write("\nDigite o Id da prescricao: ");
                bool idValido = int.TryParse(Console.ReadLine(), out idRelatorioEscolhido);

                if (!idValido)
                {
                    Notificador.ExibirMensagem("\nID inválido, selecione novamente.\n", ConsoleColor.Red);

                    continue;
                }
                else
                    break;
            }

            Console.Clear();

            PrescricaoMedica prescricaoMedica = IRepositorioPrescricaoMedica.SelecionarRegistroPorId(idRelatorioEscolhido);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Relatorio da prescricao");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine
            (
                $"Id: {prescricaoMedica.Id} \nCRMMedico: {prescricaoMedica.CRMMedico} \nPaciente: {prescricaoMedica.Paciente!.Nome} " +
                $"\nStatus: {prescricaoMedica.Status} \nData: {prescricaoMedica.Data.ToShortDateString()}"
            );
            Console.WriteLine("----------------------------------------");

            foreach (PrescricaoMedicamento prescMed in prescricaoMedica.Medicamentos)
            {
                Console.WriteLine($"Nome: {prescMed.Medicamento!.Nome}");
                Console.WriteLine($"Dosagem: {prescMed.Dosagem}");
                Console.WriteLine($"Periodo: {prescMed.Periodo}");
            }

            break;
        }
    }

    protected override void ExibirCabecalhoTabela()
    {
        if (IRepositorioPrescricaoMedica!.ListaVazia())
        {
            Notificador.ExibirMensagem("\nNenhum registro encontrado.\n", ConsoleColor.Red);
            return;
        }

        IRepositorioPrescricaoMedica!.VerificarValidade();

        Console.WriteLine
        (
            "{0, -10} | {1, -10} | {2, -15} | {3, -15} | {4, -20}",
            "ID", "CRMMedico", "Status", "Data Emissao", "Medicamentos"
        );
    }

    protected override void ExibirLinhaTabela(PrescricaoMedica pM)
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -10} | {2, -15} | {3, -15} | {4, -20}",
            pM.Id, pM.CRMMedico, pM.Status, pM.Data.ToShortDateString(), pM.Medicamentos.Count.ToString()
        );
    }
}
