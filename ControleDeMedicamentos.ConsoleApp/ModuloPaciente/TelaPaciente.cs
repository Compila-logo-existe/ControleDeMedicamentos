using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class TelaPaciente : TelaBase<Paciente>
{
    private RepositorioPacienteEmArquivo RepositorioPacienteEmArquivo { get; set; } 
    
    private TelaPrescricaoMedica TelaPrescricaoMedica { get; set; }
    private RepositorioPrescricaoMedicaEmArquivo RepositorioPrescricaoMedicaEmArquivo { get; set; }

    internal TelaPaciente
    (
        RepositorioPacienteEmArquivo repositorioPacienteEmArquivo,

        RepositorioPrescricaoMedicaEmArquivo repositorioPrescricaoMedicaEmArquivo,
        TelaPrescricaoMedica telaPrescricaoMedica

    ) : base("Paciente", repositorioPacienteEmArquivo)
    {
        RepositorioPacienteEmArquivo = repositorioPacienteEmArquivo;

        RepositorioPrescricaoMedicaEmArquivo = repositorioPrescricaoMedicaEmArquivo;
        TelaPrescricaoMedica = telaPrescricaoMedica;
    }

    public override bool TemRestricoesNoInserir(Paciente paciente, out string mensagem)
    {
        mensagem = string.Empty; 
        
        if (RepositorioPacienteEmArquivo.VerificarCartaoSUSRegistros(paciente))
        {
            mensagem += "\nJa existe um cadastro com esse Cartao do SUS!";   

            return true;
        }
        else
        {
            return false;
        }
    }

    public override Paciente ObterDados()
    {
        Console.Write("Digite o Nome: ");  
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o Telefone: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o Numero do Cartao do SUS: ");
        string cartaoSUS = Console.ReadLine() ?? string.Empty;

        List<PrescricaoMedica> prescricoesMedicas = []; 
        do 
        {
            TelaPrescricaoMedica.VisualizarRegistros(false);
            Console.Write("Digite o Id da prescricao: ");
            int idPrescricao = int.Parse(Console.ReadLine()!);  
            PrescricaoMedica prescricao = RepositorioPrescricaoMedicaEmArquivo.SelecionarRegistroPorId(idPrescricao); 
            prescricoesMedicas.Add(prescricao);

            Console.Write("Deseja atribuir mais prescricoes? (S/n): ");
            string opcao = Console.ReadLine()!.ToUpper() ?? string.Empty;

            if (opcao == "S")
            {
                continue; 
            }
            else if (opcao == "N")
            {
                return new Paciente(nome, telefone, cartaoSUS, prescricoesMedicas, null); // dxando null de placeholder
            }
        }
        while(true);
    }

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -20} | {2, -15} | {3, -18}",
            "ID", "Nome", "Telefone", "Cartao SUS"
        ); 
    }

    protected override void ExibirLinhaTabela(Paciente p)
    {
        Console.WriteLine
        (
            "{0, -10} | {1, -20} | {2, -15} | {3, -18}",
            p.Id, p.Nome, p.Telefone, p.CartaoSUS
        ); 
    }
}
