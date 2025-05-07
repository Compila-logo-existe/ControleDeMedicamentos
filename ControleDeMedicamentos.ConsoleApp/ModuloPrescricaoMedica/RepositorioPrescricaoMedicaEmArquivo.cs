using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class RepositorioPrescricaoMedicaEmArquivo : RepositorioBaseEmArquivo<PrescricaoMedica>, IRepositorioPrescricaoMedica
{
    public RepositorioPrescricaoMedicaEmArquivo(ContextoDados contexto) : base(contexto) { }

    public bool ListaVazia()
    {
        if (contexto.PrescricoesMedicas.Count <= 0)
            return true;
        else
            return false;
    }

    protected override List<PrescricaoMedica> ObterRegistros()
    {
        return contexto.PrescricoesMedicas;
    }

    public override void CadastrarRegistro(PrescricaoMedica novoRegistro)
    {
        novoRegistro.Paciente!.GuardarPrescricao(novoRegistro);

        base.CadastrarRegistro(novoRegistro);
    }

    public void VerificarValidade()
    {
        foreach (PrescricaoMedica pM in contexto.PrescricoesMedicas)
        {
            if (pM.Data?.AddDays(30) < DateTime.Now)
            {
                pM.Status = "Vencida";
            }
        }
    }
}
