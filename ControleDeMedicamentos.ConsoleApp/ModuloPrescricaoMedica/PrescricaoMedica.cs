using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class PrescricaoMedica : EntidadeBase<PrescricaoMedica>
{
    public string CRMMedico { get; set; }
    public DateTime Data { get; set; }            
    public List<MedicamentoPrescricao> Medicamentos { get; set; }
    public Paciente Paciente { get; set; }

    public PrescricaoMedica(string cRMMedico, DateTime data, List<MedicamentoPrescricao> medicamentos, Paciente paciente)
    {
        CRMMedico = cRMMedico;
        Data = data;
        Medicamentos = medicamentos;
        Paciente = paciente;
    }

    public override void AtualizarRegistro(PrescricaoMedica prescMedEditada)
    {
        CRMMedico = prescMedEditada.CRMMedico;
        Data = prescMedEditada.Data;
        Medicamentos = prescMedEditada.Medicamentos;
        Paciente = prescMedEditada.Paciente;
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
