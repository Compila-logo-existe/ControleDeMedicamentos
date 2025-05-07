using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public interface IRepositorioFuncionario : IRepositorio<Funcionario>
{
    public bool ListaVazia();

    public bool VerificarCPFInserirRegistro(Funcionario funcionario);

    public bool VerificarCPFEditarRegistro(Funcionario funcionario, Funcionario dadosEditados);
}