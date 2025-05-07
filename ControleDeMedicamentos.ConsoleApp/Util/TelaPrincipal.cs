using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

namespace ControleDeMedicamentos.ConsoleApp.Util;

public class TelaPrincipal
{
    private char opcaoPrincipal;
    private ContextoDados contexto;
    private TelaFornecedor telaFornecedor;
    private TelaFuncionario telaFuncionario;
    private TelaMedicamento telaMedicamento;
    private TelaRequisicaoEntrada telaRequisicaoEntrada;
    private TelaRequisicaoSaida telaRequisicaoSaida;
    private TelaPaciente telaPaciente;
    private TelaPrescricaoMedica telaPrescricaoMedica;

    public TelaPrincipal()
    {
        contexto = new ContextoDados(true);

        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        telaFornecedor = new TelaFornecedor(repositorioFornecedor);

        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
        telaFuncionario = new TelaFuncionario(repositorioFuncionario);

        IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        telaMedicamento = new TelaMedicamento(repositorioMedicamento, telaFornecedor);

        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
        telaPaciente = new TelaPaciente(repositorioPaciente, telaPrescricaoMedica!);

        IRepositorioPrescricaoMedica repositorioPrescricaoMedica = new RepositorioPrescricaoMedicaEmArquivo(contexto);
        telaPrescricaoMedica = new TelaPrescricaoMedica(repositorioPrescricaoMedica, telaMedicamento, telaPaciente);

        IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada = new RepositorioRequisicaoEntradaEmArquivo(contexto);
        telaRequisicaoEntrada = new TelaRequisicaoEntrada(repositorioRequisicaoEntrada, telaMedicamento, telaFuncionario);

        IRepositorioRequisicaoSaida repositorioRequisicaoSaida = new RepositorioRequisicaoSaidaEmArquivo(contexto);
        telaRequisicaoSaida = new TelaRequisicaoSaida(repositorioRequisicaoSaida, telaMedicamento, telaPaciente, telaPrescricaoMedica);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("------------------------------------------");
        Console.WriteLine("|        Controle de Medicamentos        |");
        Console.WriteLine("------------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Gestão de Fornecedores");
        Console.WriteLine("2 - Gestão de Pacientes");
        Console.WriteLine("3 - Gestão de Medicamentos");
        Console.WriteLine("4 - Gestão de Funcionários");
        Console.WriteLine("5 - Gestão de Requisições de Entrada");
        Console.WriteLine("6 - Gestão de Requisições de Saída");
        Console.WriteLine("7 - Gestão de Prescrições Médica");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoPrincipal = Console.ReadLine()!.ToUpper()[0];
    }

    public ITelaCrud ObterTela()
    {
        if (opcaoPrincipal == '1')
            return telaFornecedor;

        if (opcaoPrincipal == '2')
            return telaPaciente;

        if (opcaoPrincipal == '3')
            return telaMedicamento;

        if (opcaoPrincipal == '4')
            return telaFuncionario;

        if (opcaoPrincipal == '5')
            return telaRequisicaoEntrada;

        if (opcaoPrincipal == '6')
            return telaRequisicaoSaida;

        if (opcaoPrincipal == '7')
            return telaPrescricaoMedica;

        if (opcaoPrincipal == 'S')
            Environment.Exit(0);

        return null!;
    }
}
