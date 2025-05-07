using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
{
    public IRepositorioMedicamento RepositorioMedicamento;
    private TelaFornecedor TelaFornecedor;
    private IRepositorioRequisicaoEntrada RepositorioRequisicaoEntrada;

    public TelaMedicamento(IRepositorioMedicamento repositorio, IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada, TelaFornecedor telaFornecedor) : base("Medicamento", repositorio)
    {
        RepositorioMedicamento = repositorio;
        RepositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
        TelaFornecedor = telaFornecedor;
    }

    public override bool TemRestricoesNoInserir(Medicamento novoRegistro, out string mensagem)
    {
        mensagem = "";

        if (RepositorioMedicamento.VerificarMedicamentoNoEstoque(novoRegistro))
        {
            mensagem = $"\nMedicamento já registrado";
            mensagem += $"\nQuantidade em estoque de {novoRegistro.Nome} atualizado!";

            return true;
        }

        return false;
    }

    public override bool TemRestricoesNoEditar(Medicamento registroEscolhido, Medicamento dadosEditados, out string mensagem)
    {
        Fornecedor fornecedorAntigo = registroEscolhido.Fornecedor!;
        Fornecedor fornecedorNovo = dadosEditados.Fornecedor!;

        mensagem = "";

        if (fornecedorAntigo != fornecedorNovo)
        {
            fornecedorAntigo.RemoverMedicamento(registroEscolhido);
            fornecedorNovo.AdicionarMedicamento(registroEscolhido);
        }

        return false;
    }

    public override bool TemRestricoesNoExcluir(Medicamento registroEscolhido, out string mensagem)
    {

        mensagem = "";

        if (RepositorioMedicamento.VerificarRequisicoesMedicamento(registroEscolhido, RepositorioRequisicaoEntrada))
        {
            mensagem = "\nO Medicamento contém requisições vinculadas!\n";

            return true;
        }
        registroEscolhido.Fornecedor!.RemoverMedicamento(registroEscolhido);

        return false;
    }

    public override Medicamento ObterDados()
    {
        Console.Write("Digite o Nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        int idQuantidadeEscolhida;

        while (true)
        {
            Console.Write("Digite a Quantidade a adicionar ao estoque: ");
            bool qtdEstoque = int.TryParse(Console.ReadLine(), out idQuantidadeEscolhida);

            if (!qtdEstoque)
            {
                Notificador.ExibirMensagem("\nValor inserido está incorreto, tente novamente.\n", ConsoleColor.Red);

                continue;
            }
            else
                break;
        }

        TelaFornecedor.VisualizarRegistros(false);

        if (TelaFornecedor.RepositorioFornecedor.ListaVazia())
            return null!;

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

        Fornecedor fornecedor = TelaFornecedor.RepositorioFornecedor.SelecionarRegistroPorId(idFornecedorEscolhido);

        Console.Write("Digite a Descrição: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        Medicamento medicamento = new Medicamento(nome, idQuantidadeEscolhida, descricao, fornecedor);

        return medicamento;
    }

    protected override void ExibirCabecalhoTabela()
    {
        if (RepositorioMedicamento.ListaVazia())
        {
            Notificador.ExibirMensagem("\nNenhum registro encontrado.\n", ConsoleColor.Red);

            return;
        }

        RepositorioMedicamento.VerificarEstoque();

        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30} | {4, -20}",
            "ID", "Nome", "Qtd Estoque", "Descrição", "Status");
    }

    protected override void ExibirLinhaTabela(Medicamento registro)
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30} | {4, -20}",
                registro.Id, registro.Nome, registro.QtdEstoque, registro.Descricao, registro.Status);
    }
}
