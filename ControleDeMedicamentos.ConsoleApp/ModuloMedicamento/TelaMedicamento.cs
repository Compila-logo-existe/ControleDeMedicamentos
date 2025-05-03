using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
{
    public IRepositorioMedicamento RepositorioMedicamento;
    TelaFornecedor TelaFornecedor;

    public TelaMedicamento(IRepositorioMedicamento repositorio, TelaFornecedor telaFornecedor) : base("Medicamento", repositorio)
    {
        RepositorioMedicamento = repositorio;
        TelaFornecedor = telaFornecedor;
    }

    public override Medicamento ObterDados()
    {
        Console.Write("Digite o Nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a Quantidade em estoque: ");
        string qtdEstoque = Console.ReadLine() ?? "0";

        TelaFornecedor.VisualizarRegistros(false);

        int idFornecedorEscolhido;

        while (true)
        {
            Console.Write("Digite o ID do fornecedor do medicamento: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idFornecedorEscolhido);
            if (!idValido)
            {
                Console.WriteLine("\nID inválido, selecione novamente.\n");
                continue;
            }
            else
                break;
        }

        Fornecedor fornecedor = TelaFornecedor.RepositorioFornecedor.SelecionarRegistroPorId(idFornecedorEscolhido);

        Console.Write("Digite a Descrição: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        Medicamento medicamento = new Medicamento(nome, qtdEstoque, descricao);

        return medicamento;
    }

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30}",
                 "ID", "Nome", "Qtd Estoque", "Descrição");
    }

    protected override void ExibirLinhaTabela(Medicamento registro)
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30}",
                registro.Id, registro.Nome, registro.QtdEstoque, registro.Descricao);
    }
}
