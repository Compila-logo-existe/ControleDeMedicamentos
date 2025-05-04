using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

internal class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
{
    private RepositorioPrescricaoMedicaEmArquivo RPMEA { get; set; }

    protected TelaPrescricaoMedica(RepositorioPrescricaoMedicaEmArquivo rPMEA) : base ("Prescricao Medica", rPMEA)
    {
        RPMEA = rPMEA; 
    }

}

