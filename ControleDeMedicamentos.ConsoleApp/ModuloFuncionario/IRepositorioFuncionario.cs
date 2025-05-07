using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public interface IRepositorioFuncionario : IRepositorio<Funcionario>
{
    public bool ListaVazia();
    public bool VerificarCPFRegistros(Funcionario funcionario);
}