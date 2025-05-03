using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

public class RepositorioRequisicaoSaidaEmArquivo : RepositorioBaseEmArquivo<RequisicaoSaida>, IRepositorioRequisicaoSaida
{
    public RepositorioRequisicaoSaidaEmArquivo(ContextoDados contexto) : base(contexto) { }

    protected override List<RequisicaoSaida> ObterRegistros()
    {
        return contexto.RequisicoesSaida;
    }
}
