using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
{
    public IRepositorioPrescricaoMedica IRepositorioPrescricaoMedica { get; private set; }
    private TelaPaciente TelaPaciente { get; set; }
    public TelaMedicamentoPrescricao TelaMedicamentoPrescricao { get; private set; }

    public TelaPrescricaoMedica
    (
        IRepositorioPrescricaoMedica repositorioPrescricaoMedicaEmArquivo,

        TelaPaciente telaPaciente, 
        TelaMedicamentoPrescricao telaMedicamentoPrescricao

    ) : base 
    (
        "Prescricao Medica", 
        repositorioPrescricaoMedicaEmArquivo)
    {
        TelaPaciente = telaPaciente;
        TelaMedicamentoPrescricao = telaMedicamentoPrescricao;
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

        List<MedicamentoPrescricao> prescricoesMedicamentos = [];
        do 
        {
            TelaMedicamentoPrescricao.VisualizarRegistros(false);
            Console.Write("Digite o ID da Prescricao do Medicamento: ");
            int idPrescricaoMedicamento = int.Parse(Console.ReadLine()!);
            MedicamentoPrescricao medicamentoPrescricao = TelaMedicamentoPrescricao.IRepositorioMedicamentoPrescricao.SelecionarRegistroPorId(idPrescricaoMedicamento);

            prescricoesMedicamentos.Add(medicamentoPrescricao);
            
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
            pM.Id, pM.CRMMedico, pM.Data, pM.Medicamentos
            
        );
    }
}
