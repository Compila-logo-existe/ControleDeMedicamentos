using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class TelaPaciente : TelaBase<Paciente>, ITelaCrud
{
    public IRepositorioPaciente IRepositorioPaciente { get; set; }
    internal TelaPrescricaoMedica TelaPrescricaoMedica { get; set; }

    public TelaPaciente
    (
        IRepositorioPaciente repositorioPacienteEmArquivo,
        TelaPrescricaoMedica telaPrescricaoMedica

    ) : base("Paciente", repositorioPacienteEmArquivo)
    {
        IRepositorioPaciente = repositorioPacienteEmArquivo;
        TelaPrescricaoMedica = telaPrescricaoMedica;
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

        return new Paciente(nome, telefone, cartaoSUS);
    }

    protected override void ExibirCabecalhoTabela()
    {
        if (IRepositorioPaciente.ListaVazia())
        {
            Notificador.ExibirMensagem("Nenhum registro encontrado.", ConsoleColor.Red);
            return;
        }

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
