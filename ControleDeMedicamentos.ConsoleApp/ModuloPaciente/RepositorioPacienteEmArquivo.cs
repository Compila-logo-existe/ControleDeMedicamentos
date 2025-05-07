using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>, IRepositorioPaciente
{
    public RepositorioPacienteEmArquivo(ContextoDados contexto) : base(contexto) { }

    public bool ListaVazia()
    {
        if (contexto.Pacientes.Count <= 0)
            return true;
        else
            return false;
    }

    public bool VerificarCartaoSUSRegistros(Paciente paciente)
    {
        return contexto.Pacientes.Any(p => p != null && p.CartaoSUS == paciente.CartaoSUS);
    }

    protected override List<Paciente> ObterRegistros()
    {
        return contexto.Pacientes; 
    }
}
