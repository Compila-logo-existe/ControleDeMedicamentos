using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentoPrescricao;

internal class ModuloMedicamentoPrescricao : RepositorioBaseEmArquivo<MedicamentoPrescricao>, IRepositorioMedicamentoPrescricao
{
    protected ModuloMedicamentoPrescricao(ContextoDados contexto) : base (contexto) { }

    protected override List<MedicamentoPrescricao> ObterRegistros()
    {
        return contexto.MedicamentosPrescricoes;
    }
}
