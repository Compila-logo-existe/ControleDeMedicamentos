using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;

public class MedicamentoPrescricao : EntidadeBase<MedicamentoPrescricao>
{
    public string Dosagem { get; private set; }
    public string Periodo { get; private set; }
    public Medicamento Medicamento { get; private set; }
    public int Quantidade { get; private set; }

    internal MedicamentoPrescricao(string dos, string per, Medicamento med, int qtt)
    {
        Dosagem = dos;
        Periodo = per;
        Medicamento = med;
        Quantidade = qtt;
    }

    public override void AtualizarRegistro(MedicamentoPrescricao MedPrescEditado)
    {
        Dosagem = MedPrescEditado.Dosagem;
        Periodo = MedPrescEditado.Periodo;
        Medicamento = MedPrescEditado.Medicamento;
        Quantidade = MedPrescEditado.Quantidade;
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
