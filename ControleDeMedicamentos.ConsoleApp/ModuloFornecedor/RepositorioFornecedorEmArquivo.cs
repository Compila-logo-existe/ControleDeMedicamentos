using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

public class RepositorioFornecedorEmArquivo : RepositorioBaseEmArquivo<Fornecedor>, IRepositorioFornecedor
{
    public RepositorioFornecedorEmArquivo(ContextoDados contexto) : base(contexto) { }
    public bool ListaVazia()
    {
        if (contexto.Fornecedores.Count <= 0)
            return true;
        else
            return false;
    }

    public bool VerificarCNPJRegistros(Fornecedor fornecedor)
    {
        if (contexto.Fornecedores.Any(f => f != null && f.CNPJ == fornecedor.CNPJ))
            return true;
        else
            return false;
    }

    protected override List<Fornecedor> ObterRegistros()
    {
        return contexto.Fornecedores;
    }
}
