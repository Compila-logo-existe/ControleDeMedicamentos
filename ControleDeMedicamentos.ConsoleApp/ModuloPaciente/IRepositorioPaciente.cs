using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public interface IRepositorioPaciente : IRepositorio<Paciente>
{
    bool VerificarCartaoSUSRegistros(Paciente paciente);
}
