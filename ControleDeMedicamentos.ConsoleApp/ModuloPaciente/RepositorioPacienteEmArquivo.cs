using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

internal class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>, IRepositorioPaciente
{
    protected RepositorioPacienteEmArquivo(ContextoDados contexto) : base(contexto) {}

    public bool VerificarCartaoSUSRegistros(Paciente paciente)
    {
        return contexto.Pacientes.Any(p => p != null && p.CartaoSUS == paciente.CartaoSUS);
    }

    protected override List<Paciente> ObterRegistros()
    {
        return contexto.Pacientes; 
    }


}
