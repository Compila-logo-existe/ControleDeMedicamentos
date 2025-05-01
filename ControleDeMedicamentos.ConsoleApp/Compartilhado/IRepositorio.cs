namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public interface IRepositorio<Tipo> where Tipo : EntidadeBase<Tipo>
{
    public void CadastrarRegistro(Tipo novoRegistro);

    public bool EditarRegistro(int idRegistro, Tipo registroEditado);

    public bool ExcluirRegistro(int idRegistro);

    public List<Tipo> SelecionarRegistros();

    public Tipo SelecionarRegistroPorId(int idRegistro);
}
