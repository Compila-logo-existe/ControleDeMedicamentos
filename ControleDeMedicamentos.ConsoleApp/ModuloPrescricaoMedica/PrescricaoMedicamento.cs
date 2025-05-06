using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class PrescricaoMedicamento : EntidadeBase<PrescricaoMedicamento>
{
    public string? Dosagem { get; set; }
    public string? Periodo { get; set; }
    public Medicamento? Medicamento { get; set; }
    public int Quantidade { get; set; }

    public PrescricaoMedicamento() { }

    public PrescricaoMedicamento(string dos, string per, Medicamento med, int qtt)
    {
        Dosagem = dos;
        Periodo = per;
        Medicamento = med;
        Quantidade = qtt;
    }

    public override void AtualizarRegistro(PrescricaoMedicamento PrescMedEditado)
    {
        Dosagem = PrescMedEditado.Dosagem;
        Periodo = PrescMedEditado.Periodo;
        Medicamento = PrescMedEditado.Medicamento;
        Quantidade = PrescMedEditado.Quantidade;
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrEmpty(Dosagem))
        {
            erros += $"O campo Dosagem eh obrigatorio.\n";
        }
        else if (Dosagem.Length < 10 || Dosagem.Length > 50)
        {
            erros += $"O campo Dosagem deve ter entre 10 e 50 caracteres.\n";
        }

        if (string.IsNullOrEmpty(Periodo))
        {
            erros += $"O campo Periodo eh obrigatorio.\n";
        }
        else if (Periodo.Length < 10 || Periodo.Length > 100)
        {
            erros += $"O campo Periodo deve ter entre 10 e 100 carateres.\n";
        }

        return erros;
    }
}
