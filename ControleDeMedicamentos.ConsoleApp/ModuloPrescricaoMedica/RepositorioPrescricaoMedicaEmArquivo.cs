using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class RepositorioPrescricaoMedicaEmArquivo : RepositorioBaseEmArquivo<PrescricaoMedica>, IRepositorioPrescricaoMedica
{
    public RepositorioPrescricaoMedicaEmArquivo(ContextoDados contexto) : base (contexto) { }

    protected override List<PrescricaoMedica> ObterRegistros()
    {
        return contexto.PrescricoesMedicas;
    }

    public override void CadastrarRegistro(PrescricaoMedica novoRegistro)
    {
        novoRegistro.Paciente.GuardarPrescricao(novoRegistro);

        base.CadastrarRegistro(novoRegistro);
    }
}
