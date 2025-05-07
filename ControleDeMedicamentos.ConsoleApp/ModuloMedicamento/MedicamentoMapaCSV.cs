using CsvHelper.Configuration;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class MedicamentoMapaCSV : ClassMap<Medicamento>
{
    public MedicamentoMapaCSV()
    {
        Map(m => m.Id).Name("Id");
        Map(m => m.Nome).Name("Nome");
        Map(m => m.Descricao).Name("Descrição");
        Map(m => m.QtdEstoque).Name("Quantidade em Estoque");
        Map(m => m.Fornecedor!.CNPJ).Name("CNPJ do Fornecedor");
        Map(m => m.Fornecedor!.Nome).Name("Nome do Fornecedor");
        Map(m => m.Fornecedor!.Telefone).Name("Telefone do Fornecedor");
    }
}
