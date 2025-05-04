using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

internal class TelaPaciente : TelaBase<Paciente>, ITelaCrud
{
    private IRepositorioPaciente RepositorioPaciente { get; set; } 

    internal TelaPaciente(IRepositorioPaciente repositorio) : base("Paciente", repositorio)
    {
        RepositorioPaciente = repositorio;
    }

    public override bool TemRestricoesNoInserir(Paciente paciente, out string mensagem)
    {
        mensagem = string.Empty; 
        
        if (RepositorioPaciente.VerificarCartaoSUSRegistros(paciente))
        {
            mensagem += "\nJa existe um cadastro com esse Cartao do SUS!";   

            return true;
        }
        else
        {
            return false;
        }
    }

}
