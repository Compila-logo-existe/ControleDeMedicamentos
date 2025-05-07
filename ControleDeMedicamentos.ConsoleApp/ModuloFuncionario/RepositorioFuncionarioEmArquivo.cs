using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Funcionario>, IRepositorioFuncionario
{
    public RepositorioFuncionarioEmArquivo(ContextoDados contexto) : base(contexto) { }

    public bool ListaVazia()
    {
        if (registros.Count <= 0)
            return true;
        else
            return false;
    }

    public bool VerificarCPFInserirRegistro(Funcionario novoRegistro)
    {
        if (registros.Any(f => f != null && f.CPF == novoRegistro.CPF))
            return true;
        else
            return false;
    }

    public bool VerificarCPFEditarRegistro(Funcionario registroEscolhido, Funcionario dadosEditados)
    {
        foreach (Funcionario funcionario in registros)
        {
            if (funcionario == null)
                continue;

            if (dadosEditados.Telefone == funcionario.Telefone && dadosEditados.Id != funcionario.Id)
                return true;
        }

        return false;
    }

    protected override List<Funcionario> ObterRegistros()
    {
        return contexto.Funcionarios;
    }
}
