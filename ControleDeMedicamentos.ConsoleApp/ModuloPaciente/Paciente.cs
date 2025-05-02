using ControleDeMedicamentos.ConsoleApp.Compartilhado; 

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CartaoSUS { get; set; }
    public List<Prescricao> Prescricao { get; set; }

    // awaiting implementation
    //public List<RequisicaoSaida> RequisicoesSaida { get; set; }
    
    public override void AtualizarRegistro(Paciente paciente)
    {

    }

    public override string Validar()
    {
        return string.Empty; 
    }
}
