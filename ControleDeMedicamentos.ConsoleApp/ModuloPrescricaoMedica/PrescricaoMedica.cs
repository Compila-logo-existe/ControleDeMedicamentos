using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class PrescricaoMedica : EntidadeBase<PrescricaoMedica>
{
    private string CRMMedico { get; set; }
    private DateTime Data { get; set; }            
    private List<MedicamentoPrescricao> Medicamentos { get; set; }

    protected PrescricaoMedica(string cRMMedico, DateTime data, List<MedicamentoPrescricao> medicamentos)
    {
        CRMMedico = cRMMedico;
        Data = data;
        MedicamentoPrescricao = medicamentos;
    }

    public override void AtualizarRegistro(PrescricaoMedica prescMedEditada)
    {
        CRMMedico = prescMedEditada.CRMMedico;
        Data = prescMedEditada.Data;
        Medicamentos = prescMedEditada.Medicamentos;
    }
}
