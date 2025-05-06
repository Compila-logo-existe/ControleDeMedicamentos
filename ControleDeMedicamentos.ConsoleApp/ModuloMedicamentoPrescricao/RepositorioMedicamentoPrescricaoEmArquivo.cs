using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;

public class RepositorioMedicamentoPrescricaoEmArquivo : RepositorioBaseEmArquivo<MedicamentoPrescricao>, IRepositorioMedicamentoPrescricao
{
    protected RepositorioMedicamentoPrescricaoEmArquivo(ContextoDados contexto) : base (contexto) { }

    protected override List<MedicamentoPrescricao> ObterRegistros()
    {
        return contexto.MedicamentosPrescricoes;
    }
}
