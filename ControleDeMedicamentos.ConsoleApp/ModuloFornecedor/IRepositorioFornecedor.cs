using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

public interface IRepositorioFornecedor : IRepositorio<Fornecedor>
{
    public bool ListaVazia();
    public bool VerificarCNPJRegistros(Fornecedor fornecedor);
    public bool VerificarCNPJEditarRegistro(Fornecedor registroEscolhido, Fornecedor dadosEditados);
}
