using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

internal interface IRepositorioPaciente : IRepositorio<Paciente>
{
    public bool VerificarCartaoSUSRegistros(Paciente paciente);
}
