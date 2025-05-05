using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

internal class RepositorioPrescricaoMedicaEmArquivo : RepositorioBaseEmArquivo<PrescricaoMedica>, IRepositorioPrescricaoMedica
{
    protected RepositorioPrescricaoMedicaEmArquivo(ContextoDados contexto) : base (contexto) { }

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
