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

    public override char ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"1 - Cadastrar Prescricao Medica");
        Console.WriteLine($"2 - Gerar Relatorios de Prescricao Medica");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        char operacaoEscolhida = Convert.ToChar(Console.ReadLine()!.ToUpper());

        return operacaoEscolhida;
    }

    public override PrescricaoMedica ObterDados()
    {
        Console.WriteLine("Digite o CRM do Medico: ");
        string crmMedico = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Atribuindo data...");
        Thread.Sleep(1500);
        DateTime dataPrescricao = DateTime.Now.Date;

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

            PrescricaoMedicamento prescricaoMedicamento = new(dosagem, periodo, medicamento, quantidadeMedicacao);

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

    public void GerarRelatorio(bool exibirTitulo)
    {
        if (exibirTitulo)
            ExibirCabecalho();

        List<PrescricaoMedica> prescricoesMedicas = IRepositorioPrescricaoMedica.PegarRegistros();
        foreach (PrescricaoMedica registro in prescricoesMedicas)
        {
            VisualizarRegistros(false);

            Console.Write("Digite o Id da prescricao: ");
            int idRelatorio = int.Parse(Console.ReadLine()!);
            Console.Clear();

            PrescricaoMedica prescricaoMedica = IRepositorioPrescricaoMedica.SelecionarRegistroPorId(idRelatorio);
            
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Relatorio da prescricao");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine
            (
                $"Id: {registro.Id} \nCRMMedico: {registro.CRMMedico} \nStatus: {registro.Status} \nData: {registro.Data}"
            );
            Console.WriteLine("----------------------------------------");
           
            foreach (PrescricaoMedicamento prescMed in registro.Medicamentos)
            {
                Console.WriteLine($"Nome: {prescMed.Medicamento.Nome}");
                Console.WriteLine($"Dosagem: {prescMed.Dosagem}");
                Console.WriteLine($"Periodo: {prescMed.Periodo}");
            }

            break;
        }

        Console.WriteLine();
        Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
    }

    protected override void ExibirCabecalhoTabela()
    {
        if (IRepositorioPrescricaoMedica.ListaVazia())
        {
            Notificador.ExibirMensagem("Nenhum registro encontrado.", ConsoleColor.Red);
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
            pM.Id, pM.CRMMedico, pM.Status, pM.Data, pM.Medicamentos.Count.ToString()
        );
    }
}
