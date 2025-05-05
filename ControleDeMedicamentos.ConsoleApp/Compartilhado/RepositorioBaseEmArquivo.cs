namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class RepositorioBaseEmArquivo<Tipo> where Tipo : EntidadeBase<Tipo>
{
    protected List<Tipo> registros = new List<Tipo>();
    private int contadorIds = 0;

    protected ContextoDados contexto;

    protected RepositorioBaseEmArquivo(ContextoDados contexto)
    {
        this.contexto = contexto;

        registros = ObterRegistros();

        int maiorId = 0;

        foreach (var registro in registros)
        {
            if (registro.Id > maiorId)
                maiorId = registro.Id;
        }

        contadorIds = maiorId;
    }

    public virtual void CadastrarRegistro(Tipo novoRegistro)
    {
        novoRegistro.Id = ++contadorIds;

        registros.Add(novoRegistro);

        contexto.Salvar();
    }

    protected abstract List<Tipo> ObterRegistros();

    public bool EditarRegistro(int idRegistro, Tipo registroEditado)
    {
        foreach (Tipo item in registros)
        {
            if (item.Id == idRegistro)
            {
                item.AtualizarRegistro(registroEditado);

                contexto.Salvar();

                return true;
            }
        }

        return false;
    }

    public bool ExcluirRegistro(int idRegistro)
    {
        Tipo registroSelecionado = SelecionarRegistroPorId(idRegistro);

        if (registroSelecionado != null)
        {
            registros.Remove(registroSelecionado);

            contexto.Salvar();

            return true;
        }

        return false;
    }

    public List<Tipo> SelecionarRegistros()
    {
        return registros;
    }

    public Tipo SelecionarRegistroPorId(int idRegistro)
    {
        foreach (Tipo item in registros)
        {
            if (item.Id == idRegistro)
                return item;
        }

        return null!;
    }
}
