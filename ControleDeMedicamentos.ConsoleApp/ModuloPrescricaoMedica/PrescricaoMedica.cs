using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class PrescricaoMedica : EntidadeBase<PrescricaoMedica>
{
    public string CRMMedico { get; set; }
    public DateTime Data { get; set; }            
    public Paciente Paciente { get; set; }
    public List<MedicamentoPrescricao> Medicamentos { get; set; }

    public PrescricaoMedica(string cRMMedico, DateTime data, Paciente paciente, List<MedicamentoPrescricao> medicamentos)
    {
        CRMMedico = cRMMedico;
        Data = data;
        Paciente = paciente;
        Medicamentos = medicamentos;
    }

    public override void AtualizarRegistro(PrescricaoMedica prescMedEditada)
    {
        CRMMedico = prescMedEditada.CRMMedico;
        Data = prescMedEditada.Data;
        Paciente = prescMedEditada.Paciente;
        Medicamentos = prescMedEditada.Medicamentos;
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrWhiteSpace(CRMMedico))
        {
            erros += "O campo CRMMedico eh obrigatorio.\n"; 
        }
        else if (CRMMedico.Length != 6)
        {
            erros += "O campo CRMMedico deve conter 6 digitos.\n";
        }

        return erros;
    }
}
