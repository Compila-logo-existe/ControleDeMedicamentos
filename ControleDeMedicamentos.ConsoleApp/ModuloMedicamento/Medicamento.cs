using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class Medicamento : EntidadeBase<Medicamento>
{
    public string Nome { get; set; }
    public int QtdEstoque { get; set; }
    public string Descricao { get; set; }
    public string Status { get; set; }
    public Fornecedor Fornecedor { get; set; }

    public Medicamento(string nome, int qtdEstoque, string descricao)
    {
        Nome = nome;
        QtdEstoque = qtdEstoque;
        Descricao = descricao;
    }

    public override void AtualizarRegistro(Medicamento registroEditado)
    {
        Nome = registroEditado.Nome;
        QtdEstoque = registroEditado.QtdEstoque;
        Descricao = registroEditado.Descricao;
        Fornecedor = registroEditado.Fornecedor;
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

        if (string.IsNullOrEmpty(QtdEstoque.ToString()))
            erros += "O campo 'Quantidade em estoque' é obrigatório.\n";
        else
        {
            if (QtdEstoque <= 0)
                erros += "O campo 'Quantidade em estoque' não pode ser negativo.\n";
        }

        if (string.IsNullOrEmpty(Descricao))
            erros += "O campo 'Descriação' é obrigatório.\n";
        else
        {
            if (Descricao.Length < 5 || Descricao.Length > 255)
                erros += "O campo 'Descricao' deve conter entre 5 e 255 caracteres.\n";
        }

        if (Fornecedor == null)
            erros += "O campo 'Fornecedor' é obrigatório.";

        return erros;
    }
}
