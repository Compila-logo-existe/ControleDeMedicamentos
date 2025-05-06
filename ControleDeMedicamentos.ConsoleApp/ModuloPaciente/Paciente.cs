using System.Text.RegularExpressions;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class Paciente : EntidadeBase<Paciente>
{
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public string? CartaoSUS { get; set; }
    public List<PrescricaoMedica> Prescricoes { get; set; } = [];
    public List<RequisicaoSaida> RequisicoesSaida { get; set; } = [];

    public Paciente() { }

    public Paciente(string nome, string tel, string cartaoSus)
    {
        Nome = nome;
        Telefone = tel;
        CartaoSUS = cartaoSus;
    }

    public override void AtualizarRegistro(Paciente pacienteEditado)
    {
        Nome = pacienteEditado.Nome;
        Telefone = pacienteEditado.Telefone;
        CartaoSUS = pacienteEditado.CartaoSUS;
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrWhiteSpace(Nome))
        {
            erros += "O campo 'Nome' é obrigatório.\n";
        }
        else
        {
            if ((Nome.Length < 3) || (Nome.Length > 100))
            {
                erros += "O campo 'Nome' deve conter entre 3 e 100 caracteres.\n";
            }
        }

        if (string.IsNullOrWhiteSpace(Telefone))
        {
            erros += "O campo Nome eh obrigatorio.\n";
        }
        else
        {
            if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
            {
                erros += "O campo 'Telefone' deve seguir o padrão (DDD) 0000-0000 ou (DDD) 00000-0000.\n";
            }
        }

        if (string.IsNullOrWhiteSpace(CartaoSUS))
        {
            erros += "O campo CartaoSUS eh obrigatorio.\n";
        }
        else
        {
            if (CartaoSUS.Length != 15)
            {
                erros += "O campo CartaoSUS deve conter 15 caracteres.\n";
            }
        }

        return erros;
    }

    public void GuardarPrescricao(PrescricaoMedica novaPrescricao)
    {
        Prescricoes.Add(novaPrescricao);
    }

    public List<PrescricaoMedica> PegarPrescricoes()
    {
        return Prescricoes;
    }

    public void GuardarRequicaoDeSaida(RequisicaoSaida novaRequisicaoSaida)
    {
        RequisicoesSaida.Add(novaRequisicaoSaida);
    }

    public List<RequisicaoSaida> PegarRequisicoesSaida()
    {
        return RequisicoesSaida;
    }
}
