using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;

internal class MedicamentoPrescricao : EntidadeBase<MedicamentoPrescricao>
{
    private string Dosagem { get; set; }
    private string Periodo { get; set; }
    private Medicamento Medicamento { get; set; }
    private int Quantidade { get; set; }

    internal MedicamentoPrescricao(string dos, string per, Medicamento med, int qtt)
    {
        Dosagem = dos;
        Periodo = per;
        Medicamento = med;
        Quantidade = qtt;
    }

    public void AtualizarRegistro(MedicamentoPrescricao MedPrescEditado)
    {
        Dosagem = MedPrescEditado.Dosagem;
        Periodo = MedPrescEditado.Periodo;
        Medicamento = MedPrescEditado.Medicamento;
        Quantidade = MedPrescEditado.Quantidade;
    }

    public string Validar()
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
    }
}
