using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

public class RepositorioRequisicaoSaidaEmArquivo : RepositorioBaseEmArquivo<RequisicaoSaida>, IRepositorioRequisicaoSaida
{
    public RepositorioRequisicaoSaidaEmArquivo(ContextoDados contexto) : base(contexto) { }

    public bool ListaVazia()
    {
        if (contexto.RequisicoesSaida.Count <= 0)
            return true;
        else
            return false;
    }

    public override void CadastrarRegistro(RequisicaoSaida novoRegistro)
    {
        novoRegistro.Paciente!.GuardarRequicaoDeSaida(novoRegistro);

        base.CadastrarRegistro(novoRegistro);
    }

    public bool VerificarEstoqueExcedido(RequisicaoSaida novoRegistro)
    {
        int valor = 0;

        foreach (PrescricaoMedicamento pM in novoRegistro.MedicamentosRequisitados)
        {
            valor += pM.Quantidade;

            if (valor > pM.Medicamento!.QtdEstoque)
            {
                return true;
            }
        }

        return false;
    }

    public bool VerificarEstoquePosRequisicao(RequisicaoSaida novoRegistro)
    {
        if (novoRegistro.MedicamentosRequisitados.Any(pm => pm != null
            && pm.Medicamento!.Status == "EM FALTA!!!"))
        {
            return true;
        }

        return false;
    }

    public bool VerificarPacientePrescricao(RequisicaoSaida novoRegistro)
    {
        if (novoRegistro.PrescicaoMedica!.Paciente != novoRegistro.Paciente)
            return true;

        return false;
    }

    protected override List<RequisicaoSaida> ObterRegistros()
    {
        return contexto.RequisicoesSaida;
    }
}
