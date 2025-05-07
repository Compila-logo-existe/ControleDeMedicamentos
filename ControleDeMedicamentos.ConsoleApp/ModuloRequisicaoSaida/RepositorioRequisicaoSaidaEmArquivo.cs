using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

public class RepositorioRequisicaoSaidaEmArquivo : RepositorioBaseEmArquivo<RequisicaoSaida>, IRepositorioRequisicaoSaida
{
    public RepositorioRequisicaoSaidaEmArquivo(ContextoDados contexto) : base(contexto) { }

    public bool ListaVazia()
    {
        if (contexto.RequisicoesSaida.Count <= 0)
            return true;
        else
            return false;
    }

    public override void CadastrarRegistro(RequisicaoSaida novoRegistro)
    {
        novoRegistro.Paciente!.GuardarRequicaoDeSaida(novoRegistro);

        base.CadastrarRegistro(novoRegistro);
    }

    public bool VerificarEstoqueExcedido(RequisicaoSaida novoRegistro)
    {
        if (novoRegistro.MedicamentosRequisitados.Any(pm => pm != null
            && pm.Quantidade > pm.Medicamento!.QtdEstoque))
        {
            return true;
        }

        return false;
    }

    public bool VerificarEstoquePosRequisicao(RequisicaoSaida novoRegistro)
    {
        if (novoRegistro.MedicamentosRequisitados.Any(pm => pm != null
            && pm.Medicamento!.Status == "EM FALTA!!!"))
        {
            return true;
        }

        return false;
    }

    protected override List<RequisicaoSaida> ObterRegistros()
    {
        return contexto.RequisicoesSaida;
    }
}
