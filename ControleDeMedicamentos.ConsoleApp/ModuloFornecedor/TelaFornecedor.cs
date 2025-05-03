using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaCrud
{
    public IRepositorioFornecedor RepositorioFornecedor;

    public TelaFornecedor(IRepositorioFornecedor repositorio) : base("Fornecedor", repositorio)
    {
        RepositorioFornecedor = repositorio;
    }

    public override bool TemRestricoesNoInserir(Fornecedor novoRegistro, out string mensagem)
    {
        mensagem = "";

        if (RepositorioFornecedor.VerificarCNPJRegistros(novoRegistro))
        {
            mensagem = "\nJá existe um cadastro com esse CNPJ!";
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

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30}",
                "ID", "Nome", "Telefone", "CNPJ");
    }

    protected override void ExibirLinhaTabela(Fornecedor registro)
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30}",
                registro.Id, registro.Nome, registro.Telefone, registro.CNPJ);
    }
}
