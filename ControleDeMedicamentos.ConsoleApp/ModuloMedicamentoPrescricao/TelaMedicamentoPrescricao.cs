using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;

internal class TelaMedicamentoPrescricao : TelaBase<MedicamentoPrescricao>
{
    //private RepositorioMedicamentoPrescricaoEmArquivo RepositorioMedicamentoPrescricaoEmArquivo;
    private TelaMedicamento TelaMedicamento { get; set; }
    private RepositorioMedicamentoEmArquivo RepositorioMedicamentoEmArquivo{ get; set; }
    private RepositorioMedicamentoPrescricaoEmArquivo RepositorioMedicamentoPrescricaoEmArquivo { get; set; }

    internal TelaMedicamentoPrescricao
    (
        TelaMedicamento telaMedicamento,
        RepositorioMedicamentoEmArquivo repositorioMedicamentoEmArquivo,
        RepositorioMedicamentoPrescricaoEmArquivo repositorioMedicamentoPrescricaoEmArquivo

    ) : base ("Medicamento Prescricao", repositorioMedicamentoPrescricaoEmArquivo)
    {
        TelaMedicamento = telaMedicamento;
        RepositorioMedicamentoEmArquivo = repositorioMedicamentoEmArquivo;
        RepositorioMedicamentoPrescricaoEmArquivo = repositorioMedicamentoPrescricaoEmArquivo;
    }

    public override MedicamentoPrescricao ObterDados()
    {
        Console.Write("Digite a Dosagem: ");  
        string dosagem = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o Periodo: ");
        string periodo = Console.ReadLine() ?? string.Empty;

        TelaMedicamento.VisualizarRegistros(false);

        //Console.Write("Digite o ID do Medicamento: ");
        //int medicamentoId = int.Parse(Console.ReadLine()!);

        //Medicamento medicamento = RepositorioMedicamentoEmArquivo.SelecionarRegistroPorId(medicamentoId);
        
        // dxar comentado pq n tenho certeza q faz sentido mas creio q sim
        //
        Console.Write("Digite a Quantidade: ");
        int quantidadeMedicacao = int.Parse(Console.ReadLine()!);

        return new MedicamentoPrescricao(dosagem, periodo, _medicamentovaiaqui_, quantidadeMedicacao);
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
