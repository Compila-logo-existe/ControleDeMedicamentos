using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Funcionario>, IRepositorioFuncionario
{
    public RepositorioFuncionarioEmArquivo(ContextoDados contexto) : base(contexto) { }
    public bool ListaVazia()
    {
        if (contexto.Funcionarios.Count <= 0)
            return true;
        else
            return false;
    }
    public bool VerificarCPFRegistros(Funcionario funcionario)
    {
        if (contexto.Funcionarios.Any(f => f != null && f.CPF == funcionario.CPF))
            return true;
        else
            return false;
    }

    protected override List<Funcionario> ObterRegistros()
    {
        return contexto.Funcionarios;
    }
}
