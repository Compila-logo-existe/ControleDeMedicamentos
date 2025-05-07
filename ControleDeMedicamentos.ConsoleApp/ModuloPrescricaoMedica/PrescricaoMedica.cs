using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class PrescricaoMedica : EntidadeBase<PrescricaoMedica>
{
    public string? CRMMedico { get; set; }
    public DateTime Data { get; set; }
    public Paciente? Paciente { get; set; }
    public List<PrescricaoMedicamento> Medicamentos { get; set; } = [];
    public string? Status { get; set; } = "Disponivel";

    public PrescricaoMedica() { }

    public PrescricaoMedica(string cRMMedico, DateTime data, Paciente paciente, List<PrescricaoMedicamento> medicamentos)
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
            erros += "O campo 'CRMMedico' é obrigatório.\n";
        }
        else if (CRMMedico.Length != 6)
        {
            erros += "O campo 'CRMMedico' deve conter 6 digitos.\n";
        }
        else if (Paciente == null)
        {
            erros += "O paciente selecionado não está registrado.\n";
        }
        else if (Medicamentos == null)
        {
            erros += "O medicamento selecionado não está registrado.\n";
        }

        return erros;
    }
}
