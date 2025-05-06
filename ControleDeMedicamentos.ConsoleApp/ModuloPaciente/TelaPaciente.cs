using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

internal class TelaPaciente : TelaBase<Paciente>
{
    private IRepositorioPaciente IRepositorioPaciente { get; set; } 

    internal TelaPaciente
    (
        IRepositorioPaciente iRepositorioPaciente

    ) : base("Paciente", iRepositorioPaciente)
    {
        IRepositorioPaciente = iRepositorioPaciente;
    }

    public override bool TemRestricoesNoInserir(Paciente paciente, out string mensagem)
    {
        mensagem = string.Empty; 
        
        if (IRepositorioPaciente.VerificarCartaoSUSRegistros(paciente))
        {
            mensagem += "\nJa existe um cadastro com esse Cartao do SUS!";   

            return true;
        }
        else
        {
            return false;
        }
    }

    public override Paciente ObterDados()
    {
        Console.Write("Digite o Nome: ");  
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o Telefone: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o Numero do Cartao do SUS: ");
        string cartaoSUS = Console.ReadLine() ?? string.Empty;
        
        return new Paciente(nome, telefone, cartaoSUS, null, null); // dxando null de placeholder
    }

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -20} | {2, -15} | {3, -18}",
            "ID", "Nome", "Telefone", "Cartao SUS"
        ); 
    }

    protected override void ExibirLinhaTabela(Paciente p)
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -20} | {2, -15} | {3, -18}",
            p.Id, p.Nome, p.Telefone, p.CartaoSUS
        ); 
    }
}
