using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public interface IRepositorioPaciente : IRepositorio<Paciente>
{
    public bool ListaVazia();
    bool VerificarCartaoSUSRegistros(Paciente paciente);
}
