using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class RepositorioMedicamentoEmArquivo :RepositorioBaseEmArquivo<Medicamento>, IRepositorioMedicamento
{
    public RepositorioMedicamentoEmArquivo(ContextoDados contexto) : base(contexto) { }

    public override void CadastrarRegistro(Medicamento medicamento)
    {
        medicamento.Fornecedor.AdicionarMedicamento(medicamento);
        base.CadastrarRegistro(medicamento);
    }

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

            if (medicamento.Nome == m.Nome && medicamento.Id == 0)
            {
                m.QtdEstoque += medicamento.QtdEstoque;
                medicamento.Fornecedor.AdicionarMedicamento(medicamento);
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
