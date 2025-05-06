using System.Globalization;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;

public class RequisicaoEntrada : EntidadeBase<RequisicaoEntrada>
{
    public string? Data { get; set; }
    public Medicamento? Medicamento { get; set; }
    public Funcionario? Funcionario { get; set; }
    public int Quantidade { get; set; }

    public RequisicaoEntrada() { }

    public RequisicaoEntrada(string data, Medicamento medicamento, Funcionario funcionario, int quantidade)
    {
        Data = data ?? DateTime.Now.ToString("dd/MM/yyyy");
        Medicamento = medicamento;
        Funcionario = funcionario;
        Quantidade = quantidade;
    }

    public override void AtualizarRegistro(RequisicaoEntrada registroEditado)
    {
        Data = registroEditado.Data;
        Medicamento = registroEditado.Medicamento;
        Funcionario = registroEditado.Funcionario;
        Quantidade = registroEditado.Quantidade;
    }

    public override string Validar()
    {
        string erros = "";

        if (!DateTime.TryParseExact(Data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _))
            erros += "O campo 'Data' está inválida! Insira uma data válida (dd/MM/yyyy).\n";

        if (Medicamento == null)
            erros += "O medicamento selecionado não está registrado.\n";

        if (Funcionario == null)
            erros += "O funcionário selecionado não está registrado.\n";

        if (Quantidade <= 0)
            erros += "O campo 'Quantidade' precisa ser um valor positivo.\n";

        return erros;
    }
}
