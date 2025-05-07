namespace ControleDeMedicamentos.ConsoleApp.Util;

public static class Notificador
{
    public static void ExibirMensagem(string mensagem, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine();
        Console.Write(mensagem);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void PressioneEnter(string tipo, ConsoleColor cor = ConsoleColor.Yellow)
    {
        Console.ForegroundColor = cor;

        switch (tipo)
        {
            case "CONTINUAR":
                Console.Write("\nPressione [Enter] para continuar.");
                Console.ReadKey();
                break;
        }

        Console.ForegroundColor = ConsoleColor.White;
    }
}
