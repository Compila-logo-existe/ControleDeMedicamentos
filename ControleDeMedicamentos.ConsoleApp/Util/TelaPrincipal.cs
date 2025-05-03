using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;

namespace ControleDeMedicamentos.ConsoleApp.Util;

public class TelaPrincipal
{
    private char opcaoPrincipal;

    private ContextoDados contexto;
    private TelaFuncionario telaFuncionario;
    private TelaRequisicaoEntrada telaRequisicaoEntrada;

    public TelaPrincipal()
    {
        contexto = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
        telaFuncionario = new TelaFuncionario(repositorioFuncionario);
        IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada = new RepositorioRequisicaoEntradaEmArquivo(contexto);
        telaRequisicaoEntrada = new TelaRequisicaoEntrada(repositorioRequisicaoEntrada, telaFuncionario);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("------------------------------------------");
        Console.WriteLine("|        Controle de Medicamentos        |");
        Console.WriteLine("------------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Gestão de Funcionários");
        Console.WriteLine("2 - Gestão de Requisições de Entrada");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoPrincipal = Console.ReadLine()!.ToUpper()[0];
    }

    public ITelaCrud ObterTela()
    {
        if (opcaoPrincipal == '1')
            return telaFuncionario;

        if (opcaoPrincipal == '2')
            return telaRequisicaoEntrada;

        if (opcaoPrincipal == 'S')
            Environment.Exit(0);

        return null!;
    }
}