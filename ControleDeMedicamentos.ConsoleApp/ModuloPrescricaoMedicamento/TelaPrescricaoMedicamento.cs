using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedicamento;

internal class TelaPrescricaoMedicamento : TelaBase<PrescricaoMedicamento>, ITelaCrud
{
    //private RepositorioPrescricaoMedicamentoEmArquivo RPMEA;

    protected TelaPrescricaoMedicamento(RepositorioPrescricaoMedicamentoEmArquivo rPMEA) : base ("Prescricao Medicamento", rPMEA)
    {
        //RPMEA = rPMEA; 
    }

    public override PrescricaoMedicamento ObterDados()
    {
        Console.WriteLine("Digite o CRM do Medico: ");   
        string crmMedico = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Digite a data da prescricao (dd/MM/yyyy): ");
        DateTime dataPrescricao = DateTime.Now;

        return new PrescricaoMedicamento(crmMedico, dataPrescricao);
    }

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -10} | {2, -15} | {3, -20}",
            "ID", "CRMMedico", "Data Emissao", "Medicamentos"
        );
    }

    protected override void ExibirLinhaTabela(PrescricaoMedicamento pM)
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -10} | {2, -15} | {3, -20}",
            pM.Id, pM.CRMMedico, pM.Data, pM.Medicamentos
            
        );
    }
}
