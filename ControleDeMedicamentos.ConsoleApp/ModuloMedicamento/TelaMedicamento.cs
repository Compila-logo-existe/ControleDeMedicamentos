using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
{
    public IRepositorioMedicamento RepositorioMedicamento;
    internal TelaFornecedor TelaFornecedor;

    public TelaMedicamento(IRepositorioMedicamento repositorio, TelaFornecedor telaFornecedor) : base("Medicamento", repositorio)
    {
        RepositorioMedicamento = repositorio;
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

    public override void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Editando Medicamento...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);


        int idRegistroEscolhido;

        while (true)
        {
            Console.Write("Digite o ID do medicamento que deseja selecionar: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idRegistroEscolhido);
            if (!idValido)
            {
                Console.WriteLine("\nID inválido, selecione novamente.\n");
                continue;
            }
            else
                break;
        }

        Medicamento medicamentoOriginal = RepositorioMedicamento.SelecionarRegistroPorId(idRegistroEscolhido);
        Fornecedor fornecedorAntigo = medicamentoOriginal.Fornecedor!;

        Console.WriteLine();

        Medicamento medicamentoEditado = ObterDados();

        Fornecedor fornecedorEditado = medicamentoEditado.Fornecedor!;

        string erros = medicamentoEditado.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            EditarRegistro();

            return;
        }

        bool conseguiuEditar = RepositorioMedicamento.EditarRegistro(idRegistroEscolhido, medicamentoEditado);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Houve um erro durante a edição do registro...", ConsoleColor.Red);

            return;
        }

        if (fornecedorAntigo != fornecedorEditado)
        {
            fornecedorAntigo.RemoverMedicamento(medicamentoOriginal);
            fornecedorEditado.AdicionarMedicamento(medicamentoOriginal);
        }

        Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
    }

    public override bool TemRestricoesNoExcluir(int idRegistro, out string mensagem)
    {

        mensagem = "";

        Medicamento medicamentoEscolhido = RepositorioMedicamento.SelecionarRegistroPorId(idRegistro);

        medicamentoEscolhido.Fornecedor!.RemoverMedicamento(medicamentoEscolhido);
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
                Console.WriteLine("\nValor inserido está incorreto, tente novamente.\n");
                continue;
            }
            else
                break;
        }


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

        Medicamento medicamento = new Medicamento(nome, idQuantidadeEscolhida, descricao);

        return medicamento;
    }

    protected override void ExibirCabecalhoTabela()
    {
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
