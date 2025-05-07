using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public class TelaFuncionario : TelaBase<Funcionario>, ITelaCrud
{
    public IRepositorioFuncionario RepositorioFuncionario;
    private IRepositorioRequisicaoEntrada RepositorioRequisicaoEntrada;

    public TelaFuncionario(IRepositorioFuncionario repositorio, IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada) : base("Funcionário", repositorio)
    {
        RepositorioFuncionario = repositorio;
        RepositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
    }

    public override bool TemRestricoesNoInserir(Funcionario novoRegistro, out string mensagem)
    {
        mensagem = "";

        if (RepositorioFuncionario.VerificarCPFInserirRegistro(novoRegistro))
        {
            mensagem = "\nJá existe um cadastro com esse CPF!\n";

            return true;
        }

        return false;
    }

    public override bool TemRestricoesNoEditar(Funcionario registroEscolhido, Funcionario dadosEditados, out string mensagem)
    {
        mensagem = "";

        if (RepositorioFuncionario.VerificarCPFEditarRegistro(registroEscolhido, dadosEditados))
        {
            mensagem = "\nJá existe um cadastro com esse CPF!\n";

            return true;
        }

        return false;
    }

    public override bool TemRestricoesNoExcluir(Funcionario registroEscolhido, out string mensagem)
    {
        mensagem = "";

        if (RepositorioFuncionario.VerificarRequisicoesFuncionario(registroEscolhido, RepositorioRequisicaoEntrada))
        {
            mensagem = "\nO Funcionário contém requisições vinculadas!\n";

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
            Notificador.ExibirMensagem("\nNenhum registro encontrado.\n", ConsoleColor.Red);

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
