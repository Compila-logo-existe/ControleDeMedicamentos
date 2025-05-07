using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Medicamento>, IRepositorioMedicamento
{
    public RepositorioMedicamentoEmArquivo(ContextoDados contexto) : base(contexto) { }

    public bool ListaVazia()
    {
        if (contexto.Medicamentos.Count <= 0)
            return true;
        else
            return false;
    }

    public override void CadastrarRegistro(Medicamento novoRegistro)
    {
        novoRegistro.Fornecedor!.AdicionarMedicamento(novoRegistro);

        base.CadastrarRegistro(novoRegistro);
    }

    public void VerificarEstoque()
    {
        foreach (Medicamento m in registros)
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

    public bool VerificarMedicamentoNoEstoque(Medicamento novoRegistro)
    {
        foreach (Medicamento m in registros)
        {
            if (m == null)
                continue;

            if (novoRegistro.Nome == m.Nome && novoRegistro.Id == 0)
            {
                m.QtdEstoque += novoRegistro.QtdEstoque;
                novoRegistro.Fornecedor!.AdicionarMedicamento(novoRegistro);
                return true;
            }
        }

        return false;
    }

    public bool VerificarRequisicoesMedicamento(Medicamento registroEscolhido, IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada)
    {
        List<RequisicaoEntrada> requisicoesEntradas = repositorioRequisicaoEntrada.SelecionarRegistros();

        if (requisicoesEntradas.Any(rE => rE != null && rE.Medicamento == registroEscolhido))
            return true;

        return false;
    }

    public void AdicionarEstoque(Medicamento registroEscolhido, int qtdEstoque)
    {
        foreach (Medicamento m in registros)
        {
            if (m == null)
                continue;

            if (m.Id == registroEscolhido.Id)
            {
                m.QtdEstoque += qtdEstoque;
                return;
            }
        }
    }

    public void RemoverEstoque(PrescricaoMedica registroEscolhido)
    {
        foreach (Medicamento m in registros)
        {
            if (m == null)
                continue;

            foreach (PrescricaoMedicamento pm in registroEscolhido.Medicamentos)
            {
                if (pm == null)
                    continue;

                if (m.Id == pm.Medicamento!.Id)
                {
                    m.QtdEstoque -= pm.Quantidade;
                }
            }
        }
    }

    protected override List<Medicamento> ObterRegistros()
    {
        return contexto.Medicamentos;
    }
}
