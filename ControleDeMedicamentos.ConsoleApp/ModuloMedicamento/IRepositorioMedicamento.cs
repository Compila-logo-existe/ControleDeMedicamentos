using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public interface IRepositorioMedicamento : IRepositorio<Medicamento>
{
    public bool ListaVazia();
    public void VerificarEstoque();

    public bool VerificarMedicamentoNoEstoque(Medicamento medicamento);

    public void AdicionarEstoque(Medicamento medicamento, int qtdEstoque);

    public void RemoverEstoque(PrescricaoMedica prescricaoMedica);


}
