using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaCrud
{
    public IRepositorioRequisicaoSaida RepositorioRequisicaoSaida;
    private TelaMedicamento TelaMedicamento;
    private TelaPaciente TelaPaciente;
    private TelaPrescricaoMedica TelaPrescricaoMedica;

    public TelaRequisicaoSaida(IRepositorioRequisicaoSaida repositorioRequisicaoSaida, TelaMedicamento telaMedicamento, TelaPaciente telaPaciente, TelaPrescricaoMedica telaPrescricaoMedica) : base("Requisição de Saída", repositorioRequisicaoSaida)
    {
        TelaMedicamento = telaMedicamento;
        TelaPaciente = telaPaciente;
        TelaPrescricaoMedica = telaPrescricaoMedica;
        RepositorioRequisicaoSaida = repositorioRequisicaoSaida;
    }

    public override string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"1 - Cadastrar Requisição de Saída");
        Console.WriteLine($"2 - Visualizar Requisições de Saídas");
        Console.WriteLine($"3 - Visualizar Requisição de Saída de um Paciente");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }
    public override bool TemRestricoesNoInserir(RequisicaoSaida novoRegistro, out string mensagem)
    {
        mensagem = "";

        if (novoRegistro == null)
            return true;

        if (RepositorioRequisicaoSaida.VerificarEstoqueExcedido(novoRegistro))
        {
            mensagem = "\nEssa requisição está excedendo a quantidade dos medicamentos em estoque.\n";

            return true;
        }

        if (RepositorioRequisicaoSaida.VerificarPacientePrescricao(novoRegistro))
        {
            mensagem = "\nPaciente escolhido não corresponde ao paciente da prescrição escolhida.\n";
            return true;
        }

        novoRegistro.PegarMedicamentosRequisitados(novoRegistro.PrescicaoMedica!);
        TelaMedicamento.RepositorioMedicamento.RemoverEstoque(novoRegistro.PrescicaoMedica!);
        TelaMedicamento.RepositorioMedicamento.VerificarEstoque();

        if (RepositorioRequisicaoSaida.VerificarEstoquePosRequisicao(novoRegistro))
        {
            Notificador.ExibirMensagem("\nMedicamentos entraram em falta! Verifique o estoque.\n", ConsoleColor.Magenta);
        }

        return false;
    }
    public override RequisicaoSaida ObterDados()
    {
        string data;

        Console.Write("A data de saída é hoje? (S/n) ");
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
                Console.Write("\nDigite uma data de entrada: (dd/MM/yyyy)");
                data = Console.ReadLine()!;

                break;
            }
            else
            {
                Console.WriteLine("\nOpção inválida!\n");

                continue;
            }
        }

        TelaPaciente.VisualizarRegistros(false);

        if (TelaPaciente.IRepositorioPaciente!.ListaVazia())
            return null!;

        int idPacienteEscolhido;

        while (true)
        {
            Console.Write("\nDigite o ID de um paciente para registrar a saída: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idPacienteEscolhido);

            if (!idValido)
            {
                Console.WriteLine("\nID inválido, selecione novamente.\n");

                continue;
            }
            else
                break;
        }

        Paciente paciente = TelaPaciente.IRepositorioPaciente.SelecionarRegistroPorId(idPacienteEscolhido);

        TelaPrescricaoMedica.VisualizarRegistros(false);

        if (TelaPrescricaoMedica.IRepositorioPrescricaoMedica!.ListaVazia())
            return null!;

        int idPrescricaoMedicaEscolhida;

        while (true)
        {

            Console.Write("\nDigite o ID de uma prescrição médica para registrar a saída: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idPrescricaoMedicaEscolhida);

            if (!idValido)
            {
                Console.WriteLine("\nID inválido, selecione novamente.\n");

                continue;
            }
            else
                break;
        }

        PrescricaoMedica prescricaoMedica = TelaPrescricaoMedica.IRepositorioPrescricaoMedica!.SelecionarRegistroPorId(idPrescricaoMedicaEscolhida);

        RequisicaoSaida requisicaoSaida = new RequisicaoSaida(data, paciente, prescricaoMedica);

        return requisicaoSaida;
    }

    public void VisualizarRequisicoesPaciente()
    {
        TelaPaciente.VisualizarRegistros(false);

        int idPacienteEscolhido;

        while (true)
        {
            Console.Write("Digite o ID do paciente: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idPacienteEscolhido);

            if (!idValido)
            {
                Console.WriteLine("\nID inválido, selecione novamente.\n");

                continue;
            }
            else
                break;
        }

        Paciente paciente = TelaPaciente.IRepositorioPaciente.SelecionarRegistroPorId(idPacienteEscolhido);

        Console.WriteLine();
        Console.WriteLine($"Visualizando Requisições de Saída do {paciente.Nome}");
        Console.WriteLine("------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("{0, -10} | {1, -20} | {2, -22} | {3, -15}",
                "ID", "Data", "ID Prescrição Médica", "Qtd de Medicamentos Requisitados");

        List<RequisicaoSaida> prescricoes = paciente.PegarRequisicoesSaida();

        foreach (RequisicaoSaida r in prescricoes)
        {
            Console.WriteLine("{0, -10} | {1, -20} | {2, -22} | {3, -15}",
                r.Id, r.Data, r.PrescicaoMedica!.Id, r.MedicamentosRequisitados.Count.ToString());
        }
    }

    protected override void ExibirCabecalhoTabela()
    {
        if (RepositorioRequisicaoSaida.ListaVazia())
        {
            Notificador.ExibirMensagem("\nNenhum registro encontrado.\n", ConsoleColor.Red);
            return;
        }

        Console.WriteLine("{0, -10} | {1, -20} | {2, -20} | {3, -10} | {4, -15}",
        "ID", "Data", "Paciente", "ID Prescrição Médica", "Qtd de Medicamentos Requisitados");
    }

    protected override void ExibirLinhaTabela(RequisicaoSaida registro)
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -15}",
                registro.Id, registro.Data, registro.Paciente!.Nome, registro.PrescicaoMedica!.Id, registro.MedicamentosRequisitados.Count.ToString());
    }
}
