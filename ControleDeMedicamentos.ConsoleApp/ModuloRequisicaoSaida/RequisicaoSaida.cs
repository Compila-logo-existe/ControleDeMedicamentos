using System.Globalization;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

public class RequisicaoSaida : EntidadeBase<RequisicaoSaida>
{

    public string Data { get; set; }
    public Paciente Paciente { get; set; }
    public PrescricaoMedica PrescicaoMedica { get; set; }
    public List<Medicamento> MedicamentosRequisitados { get; set; }

    public RequisicaoSaida() { }
    public RequisicaoSaida(string data, Paciente paciente, PrescricaoMedica prescicaoMedica, List<Medicamento> medicamentosRequisitados)
    {
        Data = data ?? DateTime.Now.ToString("dd/MM/yyyy");
        Paciente = paciente;
        PrescicaoMedica = prescicaoMedica;
        MedicamentosRequisitados = medicamentosRequisitados;
    }

    public override void AtualizarRegistro(RequisicaoSaida registroEditado)
    {
        Data = registroEditado.Data;
        Paciente = registroEditado.Paciente;
        PrescicaoMedica = registroEditado.PrescicaoMedica;
        MedicamentosRequisitados = registroEditado.MedicamentosRequisitados;
    }

    public override string Validar()
    {
        string erros = "";

        if (!DateTime.TryParseExact(Data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _))
            erros += "O campo 'Data' está inválida! Insira uma data válida (dd/MM/yyyy).\n";

        if (Paciente == null)
            erros += "O paciente selecionado não está registrado.\n";

        if (PrescicaoMedica == null)
            erros += "A prescrição médica selecionada não está registrado.\n";

        if (MedicamentosRequisitados == null)
            erros += "Os medicamentos requisitados são inválidos!\n";

        return erros;
    }
}
