using System.Text.RegularExpressions;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

public class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CNPJ { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
    public int QtdMedicamentos
    {
        get
        {
            return Medicamentos.Count;
        }
    }

    public Fornecedor()
    {
        Medicamentos = new List<Medicamento>();
    }

    public Fornecedor(string nome, string telefone, string cnpj)
    {
        Nome = nome;
        Telefone = telefone;
        CNPJ = cnpj;
    }

    public override void AtualizarRegistro(Fornecedor registroEditado)
    {
        Nome = registroEditado.Nome;
        Telefone = registroEditado.Telefone;
        CNPJ = registroEditado.CNPJ;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrEmpty(Nome))
            erros += "O campo 'Nome' é obrigatório.\n";
        else
        {
            if (Nome.Length < 3 || Nome.Length > 100)
                erros += "O campo 'Nome' deve conter entre 3 e 100 caracteres.\n";
        }

        if (string.IsNullOrEmpty(Telefone))
            erros += "O campo 'Telefone' é obrigatório.\n";
        else
        {
            if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
                erros += "O campo 'Telefone' é deve seguir o padrão (DDD) 0000-0000 ou (DDD) 00000-0000.\n";
        }

        if (string.IsNullOrEmpty(CNPJ))
            erros += "O campo 'CNPJ' é obrigatório.\n";
        else
        {
            if (!Regex.IsMatch(CNPJ, @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"))
                erros += "O campo 'CNPJ' deve estar no formato 00.000.000/0000-00.\n";
        }

        return erros;
    }

    public void AdicionarMedicamento(Medicamento medicamento)
    {
        Medicamentos.Add(medicamento);
    }
    public void RemoverMedicamento(Medicamento medicamentoExcluir)
    {
        foreach (Medicamento medicamento in Medicamentos)
        {
            if (medicamentoExcluir == medicamento)
            {
                Medicamentos.Remove(medicamentoExcluir);
                return;
            }
        }
    }
    public List<Medicamento> ObterMedicamentos()
    {
        return Medicamentos;
    }
}
