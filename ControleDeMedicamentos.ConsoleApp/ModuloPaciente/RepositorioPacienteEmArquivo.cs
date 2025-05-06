using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>, IRepositorioPaciente
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
    
    public int AtribuirReceitaAoPaciente()
    {
        Console.Write("A qual paciente deseja atribuir a receita? (ID): ");     
        return int.Parse(Console.ReadLine()!);
    }
}
