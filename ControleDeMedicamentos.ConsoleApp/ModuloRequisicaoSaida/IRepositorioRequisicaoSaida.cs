using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

public interface IRepositorioRequisicaoSaida : IRepositorio<RequisicaoSaida>
{
    public bool ListaVazia();

    public bool VerificarEstoqueExcedido(RequisicaoSaida novoRegistro);

    public bool VerificarEstoquePosRequisicao(RequisicaoSaida novoRegistro);
}