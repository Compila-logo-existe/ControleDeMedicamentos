using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>, IRepositorioPaciente
{
    public RepositorioPacienteEmArquivo(ContextoDados contexto) : base(contexto) { }

    public bool ListaVazia()
    {
        if (registros.Count <= 0)
            return true;
        else
            return false;
    }

    public bool VerificarCartaoSUSInserirRegistro(Paciente paciente)
    {
        return registros.Any(p => p != null && p.CartaoSUS == paciente.CartaoSUS);
    }
    
    public bool VerificarCartaoSUSEditarRegistro(Paciente pacienteExistente, Paciente pacienteEditado)
    {
        foreach (Paciente paciente in registros)
        {
            if (paciente == null)
                continue;

            if (pacienteEditado.CartaoSUS == paciente.CartaoSUS && pacienteExistente.Id != pacienteEditado.Id)
                return true;
        }
        return false;
    }

    protected override List<Paciente> ObterRegistros()
    {
        return contexto.Pacientes; 
    }
}
