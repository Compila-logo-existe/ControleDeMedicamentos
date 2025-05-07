using System.Text.RegularExpressions;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public class Funcionario : EntidadeBase<Funcionario>
{
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public string? CPF { get; set; }

    public Funcionario() { }

    public Funcionario(string nome, string telefone, string cPF)
    {
        Nome = nome;
        Telefone = telefone;
        CPF = cPF;
    }
    public override void AtualizarRegistro(Funcionario registroEditado)
    {
        Nome = registroEditado.Nome;
        Telefone = registroEditado.Telefone;
        CPF = registroEditado.CPF;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "O campo 'Nome' é obrigatório.\n";
        else
        {
            if (Nome.Length < 3 || Nome.Length > 100)
                erros += "O campo 'Nome' deve conter entre 3 e 100 caracteres.\n";
        }

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo 'Telefone' é obrigatório.\n";
        else
        {
            if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
                erros += "O campo 'Telefone' deve seguir o padrão (DDD) 0000-0000 ou (DDD) 00000-0000.\n";
        }

        if (string.IsNullOrWhiteSpace(CPF))
            erros += "O campo 'CPF' é obrigatório.\n";
        else
        {
            if (!Regex.IsMatch(CPF, @"^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$"))
                erros += "O campo 'CPF' precisa conter 11 números.\n";
        }

        return erros;
    }
}
