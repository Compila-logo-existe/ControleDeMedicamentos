using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public interface IRepositorioPrescricaoMedica : IRepositorio<PrescricaoMedica> 
{
    public bool ListaVazia();
    public void VerificarValidade();

    public List<PrescricaoMedica> PegarRegistros();
}
