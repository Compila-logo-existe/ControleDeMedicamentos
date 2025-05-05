using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
{
    protected TelaPrescricaoMedica(IRepositorioPrescricaoMedica repositorio) : base ("Prescricao Medica", repositorio)
    {
    }

    public override PrescricaoMedica ObterDados()
    {
        Console.WriteLine("Digite o CRM do Medico: ");   
        string crmMedico = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Id > ");
        string id = Console.ReadLine();

        PrescricaoMedica prescricaoMedica = repositorio.SelecionarRegistroPorId(id); 

        Console.WriteLine("Digite a data da prescricao (dd/MM/yyyy): ");
        DateTime dataPrescricao = DateTime.Now;

        //implementar logica pra selecionar o paciente a qual a prescricao se aplica
        //implementar logica pra atriuir medicamentos a lista de medicamentos da PrescricaoMedica

        return new PrescricaoMedica(crmMedico, dataPrescricao, null, null); //dxando null p parar de encher o saco por enqnt;
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
