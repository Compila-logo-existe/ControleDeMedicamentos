using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;

public class RepositorioRequisicaoEntradaEmArquivo : RepositorioBaseEmArquivo<RequisicaoEntrada>, IRepositorioRequisicaoEntrada
{
    public RepositorioRequisicaoEntradaEmArquivo(ContextoDados contexto) : base(contexto) { }

    public bool ListaVazia()
    {
        if (contexto.RequisicoesEntrada.Count <= 0)
            return true;
        else
            return false;
    }

    protected override List<RequisicaoEntrada> ObterRegistros()
    {
        return contexto.RequisicoesEntrada;
    }
}
