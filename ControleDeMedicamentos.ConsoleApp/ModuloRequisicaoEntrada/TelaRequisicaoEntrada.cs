using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;

public class TelaRequisicaoEntrada : TelaBase<RequisicaoEntrada>, ITelaCrud
{
    TelaFuncionario TelaFuncionario;
    TelaMedicamento TelaMedicamento;
    IRepositorioRequisicaoEntrada RepositorioRequisicaoEntrada;

    public TelaRequisicaoEntrada(IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada, TelaMedicamento telaMedicamento, TelaFuncionario telaFuncionario) : base("Requisicão de Entrada", repositorioRequisicaoEntrada)
    {
        TelaFuncionario = telaFuncionario;
        TelaMedicamento = telaMedicamento;
        RepositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
    }

    public override RequisicaoEntrada ObterDados()
    {
        string data;

        Console.Write("A data de entrada é hoje? (S/n) ");
        string opcao = Console.ReadLine() ?? string.Empty;

        while (true)
        {
            if (!string.IsNullOrEmpty(opcao) && opcao.ToUpper() == "S")
            {
                data = default!;
                break;
            }
            if (!string.IsNullOrWhiteSpace(opcao) && opcao.ToLower() == "n")
            {
                Console.Write("Digite uma data de entrada: (dd/MM/yyyy)");
                data = Console.ReadLine()!;
                break;
            }
            else
            {
                Console.WriteLine("\nOpção inválida!\n");
                continue;
            }
        }

        TelaMedicamento.VisualizarRegistros(false);

        int idMedicamentoEscolhido;

        while (true)
        {
            Console.Write("Digite o ID de um medicamento para dar entrada: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idMedicamentoEscolhido);
            if (!idValido)
            {
                Console.WriteLine("\nID inválido, selecione novamente.\n");
                continue;
            }
            else
                break;
        }

        Medicamento medicamento = TelaMedicamento.RepositorioMedicamento.SelecionarRegistroPorId(idMedicamentoEscolhido);

        TelaFuncionario.VisualizarRegistros(false);

        int idFuncionarioEscolhido;

        while (true)
        {
            Console.Write("Digite o ID de um funcionário para dar entrada: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idFuncionarioEscolhido);
            if (!idValido)
            {
                Console.WriteLine("\nID inválido, selecione novamente.\n");
                continue;
            }
            else
                break;
        }

        Funcionario funcionario = TelaFuncionario.RepositorioFuncionario.SelecionarRegistroPorId(idMedicamentoEscolhido);

        int quantidade;

        while (true)
        {
            Console.Write("Digite a quantidade que será armazenado ao estoque: ");
            bool idValido = int.TryParse(Console.ReadLine(), out quantidade);
            if (!idValido)
            {
                Console.WriteLine("\nQuantidade inválida, selecione novamente.\n");
                continue;
            }
            else
                break;
        }

        RequisicaoEntrada requisicaoEntrada = new RequisicaoEntrada(data, medicamento, funcionario, quantidade);

        return requisicaoEntrada;
    }

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine("{0, -10} | {1, -15} | {2, -20} | {3, -20} | {4, -15}",
                "ID", "Data", "Medicamento", "Funcionário", "Quantidade");
    }

    protected override void ExibirLinhaTabela(RequisicaoEntrada registro)
    {
        Console.WriteLine("{0, -10} | {1, -15} | {2, -20} | {3, -20} | {4, -15}",
                registro.Id, registro.Data, registro.Medicamento.Nome, registro.Funcionario.Nome, registro.Quantidade);
    }
}
