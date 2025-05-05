using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Medicamento>, IRepositorioMedicamento
{
    public RepositorioMedicamentoEmArquivo(ContextoDados contexto) : base(contexto) { }

    public void VerificarEstoque()
    {
        foreach (Medicamento m in contexto.Medicamentos)
        {
            if (m == null)
                continue;

            if (m.QtdEstoque >= 20)
                m.Status = "Em estoque";

            else
                m.Status = "EM FALTA!!!";
        }
        contexto.Salvar();

    }

    public bool VerificarMedicamentoNoEstoque(Medicamento medicamento)
    {
        foreach (Medicamento m in contexto.Medicamentos)
        {
            if (m == null)
                continue;

            if (medicamento.Id == m.Id && medicamento.Id == 0)
            {
                m.QtdEstoque += medicamento.QtdEstoque;
                return true;
            }
        }

        return false;
    }

    public void AdicionarEstoque(Medicamento medicamento, int qtdEstoque)
    {
        foreach (Medicamento m in contexto.Medicamentos)
        {
            if (m == null)
                continue;

            if (m.Id == medicamento.Id)
            {
                m.QtdEstoque += qtdEstoque;
                return;
            }
        }
    }

    public void RemoverEstoque(PrescricaoMedica prescricaoMedica)
    {
        foreach (Medicamento m in contexto.Medicamentos)
        {
            if (m == null)
                continue;

            foreach (MedicamentoPrescricao mp in prescricaoMedica.Medicamentos)
            {
                if (mp == null)
                    continue;

                if (m.Id == mp.Medicamento.Id)
                {
                    m.QtdEstoque -= mp.Quantidade;
                }
            }
        }
    }

    protected override List<Medicamento> ObterRegistros()
    {
        return contexto.Medicamentos;
    }
}
