using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class TelaBase<Tipo> where Tipo : EntidadeBase<Tipo>
{
    protected string nomeEntidade;
    protected IRepositorio<Tipo> repositorio;

    protected TelaBase(string nomeEntidade, IRepositorio<Tipo> repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public virtual void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Controle de {nomeEntidade}");
        Console.WriteLine("--------------------------------------------");
    }

    public virtual char ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"1 - Cadastrar {nomeEntidade}");
        Console.WriteLine($"2 - Editar {nomeEntidade}");
        Console.WriteLine($"3 - Excluir {nomeEntidade}");
        Console.WriteLine($"4 - Visualizar {nomeEntidade}s");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        char operacaoEscolhida = Convert.ToChar(Console.ReadLine()!.ToUpper());

        return operacaoEscolhida;
    }

    public void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"Cadastrando {nomeEntidade}...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Tipo novoRegistro = ObterDados();

        string erros = novoRegistro.Validar();

        string mensagem;

        if (TemRestricoesNoInserir(novoRegistro, out mensagem))
        {
            Notificador.ExibirMensagem(mensagem, ConsoleColor.Red);
            return;
        }

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            CadastrarRegistro();

            return;
        }

        repositorio.CadastrarRegistro(novoRegistro);

        Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);
    }

    public virtual void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Editando {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        bool idValido;
        int idRegistroEscolhido;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Digite o ID do registro que deseja editar: ");
            idValido = int.TryParse(Console.ReadLine(), out idRegistroEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID inserido é inválido!", ConsoleColor.Red);
                return;
            }
        } while (!idValido);

        Tipo registroEscolhido = repositorio.SelecionarRegistroPorId(idRegistroEscolhido);

        Console.WriteLine();

        if (registroEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID inserido não está registrado.", ConsoleColor.Red);
            return;
        }

        Tipo registroEditado = ObterDados();

        string erros = registroEditado.Validar();

        string mensagem;

        if (TemRestricoesNoEditar(registroEscolhido, registroEditado, out mensagem))
        {
            Notificador.ExibirMensagem(mensagem, ConsoleColor.Red);
            return;
        }

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            EditarRegistro();

            return;
        }

        bool conseguiuEditar = repositorio.EditarRegistro(idRegistroEscolhido, registroEditado);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Houve um erro durante a edição do registro...", ConsoleColor.Red);

            return;
        }

        Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
    }

    public virtual void ExcluirRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Excluindo {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        bool idValido;
        int idRegistroEscolhido;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Digite o ID do registro que deseja editar: ");
            idValido = int.TryParse(Console.ReadLine(), out idRegistroEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID inserido é inválido!", ConsoleColor.Red);
                return;
            }
        } while (!idValido);

        Tipo registroEscolhido = repositorio.SelecionarRegistroPorId(idRegistroEscolhido);

        Console.WriteLine();

        string mensagem;

        if (TemRestricoesNoExcluir(registroEscolhido, out mensagem))
        {
            Notificador.ExibirMensagem(mensagem, ConsoleColor.Red);
            return;
        }

        bool conseguiuExcluir = repositorio.ExcluirRegistro(idRegistroEscolhido);

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Houve um erro durante a exclusão do registro...", ConsoleColor.Red);

            return;
        }

        Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
    }

    public void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
            ExibirCabecalho();

        Console.WriteLine($"Visualizando {nomeEntidade}s...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        ExibirCabecalhoTabela();

        List<Tipo> registros = repositorio.SelecionarRegistros();

        foreach (Tipo registro in registros)
            ExibirLinhaTabela(registro);

        Console.WriteLine();

        Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
    }

    public abstract Tipo ObterDados();

    public virtual bool TemRestricoesNoInserir(Tipo novoRegistro, out string mensagem)
    {
        mensagem = "";

        return false;
    }

    public virtual bool TemRestricoesNoEditar(Tipo registroEscolhido, Tipo dadosEditados, out string mensagem)
    {
        mensagem = "";

        return false;
    }

    public virtual bool TemRestricoesNoExcluir(Tipo registroEscolhido, out string mensagem)
    {
        mensagem = "";

        return false;
    }

    protected abstract void ExibirCabecalhoTabela();

    protected abstract void ExibirLinhaTabela(Tipo registro);
}
