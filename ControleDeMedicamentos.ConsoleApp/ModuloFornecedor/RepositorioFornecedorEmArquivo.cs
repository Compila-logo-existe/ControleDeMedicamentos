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

    public bool VerificarCNPJRegistros(Fornecedor novoRegistro)
    {
        if (contexto.Fornecedores.Any(f => f != null && f.CNPJ == novoRegistro.CNPJ))
            return true;
        else
            return false;
    }

    public bool VerificarCNPJEditarRegistro(Fornecedor registroEscolhido, Fornecedor dadosEditados)
    {
        foreach (Fornecedor fornecedor in registros)
        {
            if (fornecedor == null)
                continue;

            if (dadosEditados.CNPJ == fornecedor.CNPJ && registroEscolhido.Id != fornecedor.Id)
                return true;
        }

        return false;
    }

    protected override List<Fornecedor> ObterRegistros()
    {
        return contexto.Fornecedores;
    }
}
