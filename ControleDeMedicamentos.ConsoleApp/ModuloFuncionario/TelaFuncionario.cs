using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public class TelaFuncionario : TelaBase<Funcionario>, ITelaCrud
{
    public IRepositorioFuncionario RepositorioFuncionario;

    public TelaFuncionario(IRepositorioFuncionario repositorio) : base("Funcionário", repositorio)
    {
        RepositorioFuncionario = repositorio;
    }

    public override bool TemRestricoesNoInserir(Funcionario novoRegistro, out string mensagem)
    {
        mensagem = "";

        if (RepositorioFuncionario.VerificarCPFRegistros(novoRegistro))
        {
            mensagem = "\nJá existe um cadastro com esse CPF!";
            return true;
        }

        return false;
    }

    public override Funcionario ObterDados()
    {
        Console.Write("Digite o Nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o Telefone: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o CPF: ");
        string cPF = Console.ReadLine() ?? string.Empty;

        Funcionario funcionario = new Funcionario(nome, telefone, cPF);

        return funcionario;
    }

    protected override void ExibirCabecalhoTabela()
    {
        if (RepositorioFuncionario.ListaVazia())
        {
            Notificador.ExibirMensagem("Nenhum registro encontrado.", ConsoleColor.Red);
            return;
        }

        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30}",
                "ID", "Nome", "Telefone", "CPF");
    }

    protected override void ExibirLinhaTabela(Funcionario registro)
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30}",
                registro.Id, registro.Nome, registro.Telefone, registro.CPF);
    }
}
