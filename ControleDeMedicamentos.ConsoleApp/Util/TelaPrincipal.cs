using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.Util;

public class TelaPrincipal
{
    private char opcaoPrincipal;

    private ContextoDados contexto;

    public TelaPrincipal()
    {
        contexto = new ContextoDados(true);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("------------------------------------------");
        Console.WriteLine("|        Controle de Medicamentos        |");
        Console.WriteLine("------------------------------------------");

        Console.WriteLine();

        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoPrincipal = Console.ReadLine()![0];
    }

    public ITelaCrud ObterTela()
    {
        return null!;
    }
}