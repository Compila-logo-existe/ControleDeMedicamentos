using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaCrud
{
    public IRepositorioFornecedor RepositorioFornecedor;

    public TelaFornecedor(IRepositorioFornecedor repositorio) : base("Fornecedor", repositorio)
    {
        RepositorioFornecedor = repositorio;
    }

    public override string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"1 - Cadastrar Fornecedor");
        Console.WriteLine($"2 - Editar Fornecedor");
        Console.WriteLine($"3 - Excluir Fornecedor");
        Console.WriteLine($"4 - Visualizar Fornecedores");
        Console.WriteLine($"5 - Visualizar Medicamentos do Fornecedor");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }

    public override bool TemRestricoesNoInserir(Fornecedor novoRegistro, out string mensagem)
    {
        mensagem = "";

        if (RepositorioFornecedor.VerificarCNPJInserirRegistro(novoRegistro))
        {
            mensagem = "\nJá existe um cadastro com esse CNPJ!\n";

            return true;
        }

        return false;
    }

    public override bool TemRestricoesNoEditar(Fornecedor registroEscolhido, Fornecedor dadosEditados, out string mensagem)
    {
        mensagem = "";

        if (RepositorioFornecedor.VerificarCNPJEditarRegistro(registroEscolhido, dadosEditados))
        {
            mensagem = "\nJá existe um cadastro com esse CNPJ!\n";

            return true;
        }

        return false;
    }

    public override bool TemRestricoesNoExcluir(Fornecedor registroEscolhido, out string mensagem)
    {
        mensagem = "";

        if (registroEscolhido.QtdMedicamentos >= 1)
        {
            mensagem = "\nO Fornecedor ainda tem medicamentos vinculados!\n";

            return true;
        }

        return false;
    }

    public override Fornecedor ObterDados()
    {
        Console.Write("Digite o Nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o Telefone: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o CNPJ: ");
        string cnpj = Console.ReadLine() ?? string.Empty;

        Fornecedor fornecedor = new Fornecedor(nome, telefone, cnpj);

        return fornecedor;
    }

    public void VisualizarMedicamentosFornecedor()
    {
        if (RepositorioFornecedor.ListaVazia())
        {
            Notificador.ExibirMensagem("\nNenhum registro encontrado.\n", ConsoleColor.Red);

            return;
        }

        VisualizarRegistros(false);

        int idFornecedorEscolhido;

        while (true)
        {
            Console.Write("\nDigite o ID do fornecedor do medicamento: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idFornecedorEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nID inválido, selecione novamente.\n", ConsoleColor.Red);

                continue;
            }
            else
                break;
        }

        Fornecedor fornecedor = RepositorioFornecedor.SelecionarRegistroPorId(idFornecedorEscolhido);

        if (fornecedor == null)
        {
            Notificador.ExibirMensagem($"\nNenhum Fornecedor registrado nesse ID.\n", ConsoleColor.Red);

            return;
        }

        if (fornecedor.Medicamentos.Count <= 0)
        {
            Notificador.ExibirMensagem($"\nNenhum medicamento do {fornecedor.Nome} foi encontrado..\n", ConsoleColor.Red);

            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Visualizando Medicamentos do {fornecedor.Nome}");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30}",
            "ID", "Nome", "Qtd Vendida", "Descrição");

        List<Medicamento> medicamentos = fornecedor.ObterMedicamentos();

        foreach (Medicamento m in medicamentos)
        {
            Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30}",
            m.Id, m.Nome, m.QtdEstoque, m.Descricao);
        }
    }

    protected override void ExibirCabecalhoTabela()
    {
        if (RepositorioFornecedor.ListaVazia())
        {
            Notificador.ExibirMensagem("\nNenhum registro encontrado.\n", ConsoleColor.Red);

            return;
        }

        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30}",
                "ID", "Nome", "Telefone", "CNPJ");
    }

    protected override void ExibirLinhaTabela(Fornecedor registro)
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30}",
                registro.Id, registro.Nome, registro.Telefone, registro.CNPJ);
    }
}
