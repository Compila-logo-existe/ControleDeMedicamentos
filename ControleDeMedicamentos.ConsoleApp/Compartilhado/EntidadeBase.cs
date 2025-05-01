namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class EntidadeBase<Tipo>
{
    public int Id { get; set; }

    public abstract void AtualizarRegistro(Tipo registroEditado);
    public abstract string Validar();
}
