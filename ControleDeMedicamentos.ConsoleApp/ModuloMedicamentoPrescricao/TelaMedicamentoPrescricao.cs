using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;

public class TelaMedicamentoPrescricao : TelaBase<MedicamentoPrescricao>, ITelaCrud
{
    public IRepositorioMedicamentoPrescricao IRepositorioMedicamentoPrescricao { get; private set; }
    public TelaMedicamento TelaMedicamento { get; private set; }

    public TelaMedicamentoPrescricao
    (
        IRepositorioMedicamentoPrescricao repositorioMedicamentoPrescricaoEmArquivo,
        TelaMedicamento telaMedicamento

    ) : base ("Medicamento Prescricao", repositorioMedicamentoPrescricaoEmArquivo)
    {
        IRepositorioMedicamentoPrescricao = repositorioMedicamentoPrescricaoEmArquivo;
        TelaMedicamento = telaMedicamento;
    }

    public override MedicamentoPrescricao ObterDados()
    {
        Console.Write("Digite a Dosagem: ");  
        string dosagem = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o Periodo: ");
        string periodo = Console.ReadLine() ?? string.Empty;

        TelaMedicamento.VisualizarRegistros(false);

        Console.Write("Digite o Medicamento: ");
        int idMedicamento = int.Parse(Console.ReadLine()!);
        Medicamento medicamento = TelaMedicamento.RepositorioMedicamento.SelecionarRegistroPorId(idMedicamento);

        Console.Write("Digite a Quantidade: ");
        int quantidadeMedicacao = int.Parse(Console.ReadLine()!);

        return new MedicamentoPrescricao(dosagem, periodo, medicamento, quantidadeMedicacao);
    }

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -20} | {2, -15} | {3, -15} | {4, -18}",
            "ID", "Dosagem", "Periodo", "Remedio", "Quantidade"
        ); 
    }

    protected override void ExibirLinhaTabela(MedicamentoPrescricao mP)
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -20} | {2, -15} | {3, -15} | {4, -18}",
            mP.Id, mP.Dosagem, mP.Periodo, mP.Medicamento, mP.Quantidade
        ); 
    }
}
