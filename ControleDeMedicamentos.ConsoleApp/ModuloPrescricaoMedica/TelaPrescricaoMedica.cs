using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

internal class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
{
    private RepositorioPrescricaoMedicaEmArquivo RPMEA { get; set; }

    protected TelaPrescricaoMedica(RepositorioPrescricaoMedicaEmArquivo rPMEA) : base ("Prescricao Medica", rPMEA)
    {
        RPMEA = rPMEA; 
    }

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -10} | {2, -15} | {3, -20}",
            "ID", "CRMMedico", "Data Emissao", "Medicamentos"
        );
    }
}

