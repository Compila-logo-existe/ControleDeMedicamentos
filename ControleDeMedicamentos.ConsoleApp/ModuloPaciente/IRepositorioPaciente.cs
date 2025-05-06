using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

internal interface IRepositorioPaciente : IRepositorio<Paciente>
{
    bool VerificarCartaoSUSRegistros(Paciente paciente);
}
