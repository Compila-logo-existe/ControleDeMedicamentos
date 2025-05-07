using System.Globalization;
using System.Text;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ConsoleApp.Util;
using CsvHelper;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
{
    public IRepositorioMedicamento RepositorioMedicamento;
    private TelaFornecedor TelaFornecedor;
    private IRepositorioRequisicaoEntrada RepositorioRequisicaoEntrada;

    public TelaMedicamento(IRepositorioMedicamento repositorio, IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada, TelaFornecedor telaFornecedor) : base("Medicamento", repositorio)
    {
        RepositorioMedicamento = repositorio;
        RepositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
        TelaFornecedor = telaFornecedor;
    }
    public override string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"1 - Cadastrar Medicamento");
        Console.WriteLine($"2 - Editar Medicamento");
        Console.WriteLine($"3 - Excluir Medicamento");
        Console.WriteLine($"4 - Visualizar Medicamentos");
        Console.WriteLine($"5 - Exportar Medicamentos Para CSV");
        Console.WriteLine($"6 - Exportar Medicamentos Para PDF");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }
    public override bool TemRestricoesNoInserir(Medicamento novoRegistro, out string mensagem)
    {
        mensagem = "";

        if (RepositorioMedicamento.VerificarMedicamentoNoEstoque(novoRegistro))
        {
            mensagem = $"\nMedicamento já registrado";
            mensagem += $"\nQuantidade em estoque de {novoRegistro.Nome} atualizado!";

            return true;
        }

        return false;
    }

    public override bool TemRestricoesNoEditar(Medicamento registroEscolhido, Medicamento dadosEditados, out string mensagem)
    {
        Fornecedor fornecedorAntigo = registroEscolhido.Fornecedor!;
        Fornecedor fornecedorNovo = dadosEditados.Fornecedor!;

        mensagem = "";

        if (fornecedorAntigo != fornecedorNovo)
        {
            fornecedorAntigo.RemoverMedicamento(registroEscolhido);
            fornecedorNovo.AdicionarMedicamento(registroEscolhido);
        }

        return false;
    }

    public override bool TemRestricoesNoExcluir(Medicamento registroEscolhido, out string mensagem)
    {

        mensagem = "";

        if (RepositorioMedicamento.VerificarRequisicoesMedicamento(registroEscolhido, RepositorioRequisicaoEntrada))
        {
            mensagem = "\nO Medicamento contém requisições vinculadas!\n";

            return true;
        }
        registroEscolhido.Fornecedor!.RemoverMedicamento(registroEscolhido);

        return false;
    }

    public override Medicamento ObterDados()
    {
        Console.Write("Digite o Nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        int idQuantidadeEscolhida;

        while (true)
        {
            Console.Write("Digite a Quantidade a adicionar ao estoque: ");
            bool qtdEstoque = int.TryParse(Console.ReadLine(), out idQuantidadeEscolhida);

            if (!qtdEstoque)
            {
                Notificador.ExibirMensagem("\nValor inserido está incorreto, tente novamente.\n", ConsoleColor.Red);

                continue;
            }
            else
                break;
        }

        TelaFornecedor.VisualizarRegistros(false);

        if (TelaFornecedor.RepositorioFornecedor.ListaVazia())
            return null!;

        int idFornecedorEscolhido;

        while (true)
        {

            Console.Write("\nDigite o ID do fornecedor do medicamento: ");
            bool idValido = int.TryParse(Console.ReadLine(), out idFornecedorEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nID inválido, selecione novamente.\n", ConsoleColor.Red);

                continue;
            }
            else
                break;
        }

        Fornecedor fornecedor = TelaFornecedor.RepositorioFornecedor.SelecionarRegistroPorId(idFornecedorEscolhido);

        Console.Write("Digite a Descrição: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        Medicamento medicamento = new Medicamento(nome, idQuantidadeEscolhida, descricao, fornecedor);

        return medicamento;
    }

    public void ExtrairParaCSV()
    {
        string csvPath = Path.Combine(@"C:\\temp", $"medicamentos-{DateTime.Now:yyyyMMdd_HHmmss}.csv");

        using var writer = new StreamWriter(csvPath, false, new UTF8Encoding(true)); // UTF-8 com BOM
        using var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Quote = '"'
        });

        List<Medicamento> medicamentos = RepositorioMedicamento.SelecionarRegistros();

        csv.Context.RegisterClassMap<MedicamentoMapaCSV>();
        csv.WriteHeader<Medicamento>();
        csv.NextRecord();
        csv.WriteRecords(medicamentos);

        Notificador.ExibirMensagem("\nCSV exportado com sucesso!\n", ConsoleColor.Green);
    }

    public void ExtrairParaPDF()
    {
        string pdfPath = Path.Combine(@"C:\\temp", $"medicamentos-{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

        using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            Document doc = new Document(PageSize.A4, 30, 30, 48, 48);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();

            Paragraph titulo = new Paragraph($"Lista de Medicamentos - {DateTime.Now:dd/MM/yyyy}", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
            titulo.Alignment = Element.ALIGN_CENTER;

            doc.Add(titulo);
            doc.Add(new Paragraph("\n"));

            PdfPTable tabela = new PdfPTable(7);
            tabela.WidthPercentage = 100;
            tabela.SetWidths([0.6f, 2, 3, 1.7f, 3, 2, 2]);

            string[] cabecalhos = {
                "Id", "Nome", "Descrição", "Quantidade em Estoque",
                "CNPJ do Fornecedor", "Nome do Fornecedor", "Telefone do Fornecedor"
            };

            foreach (string cab in cabecalhos)
            {
                PdfPCell cell = new PdfPCell(new Phrase(cab, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)))
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };

                tabela.AddCell(cell);
            }

            List<Medicamento> medicamentos = RepositorioMedicamento.SelecionarRegistros();

            foreach (Medicamento m in medicamentos)
            {
                tabela.AddCell(m.Id.ToString());
                tabela.AddCell(m.Nome);
                tabela.AddCell(m.Descricao);

                PdfPCell qtdEstoqueCell = new PdfPCell(new Phrase(m.QtdEstoque.ToString()));

                if (m.QtdEstoque < 5)
                    qtdEstoqueCell.Phrase.Font.Color = BaseColor.RED;

                tabela.AddCell(qtdEstoqueCell);

                tabela.AddCell(m.Fornecedor?.CNPJ ?? "");
                tabela.AddCell(m.Fornecedor?.Nome ?? "");
                tabela.AddCell(m.Fornecedor?.Telefone ?? "");
            }

            doc.Add(tabela);

            doc.Add(new Paragraph("\n"));
            Paragraph rodape = new Paragraph(
                $"PDF gerado em {DateTime.Now:dd/MM/yyyy HH:mm:ss} - Total de registros: {medicamentos.Count}",
                FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 10, BaseColor.DARK_GRAY)
            );

            rodape.Alignment = Element.ALIGN_RIGHT;
            doc.Add(rodape);

            doc.Close();
        }

        Notificador.ExibirMensagem("\nPDF exportado com sucesso!\n", ConsoleColor.Green);
    }

    protected override void ExibirCabecalhoTabela()
    {
        if (RepositorioMedicamento.ListaVazia())
        {
            Notificador.ExibirMensagem("\nNenhum registro encontrado.\n", ConsoleColor.Red);

            return;
        }

        RepositorioMedicamento.VerificarEstoque();

        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30} | {4, -20}",
            "ID", "Nome", "Qtd Estoque", "Descrição", "Status");
    }

    protected override void ExibirLinhaTabela(Medicamento registro)
    {
        Console.WriteLine("{0, -10} | {1, -20} | {2, -15} | {3, -30} | {4, -20}",
                registro.Id, registro.Nome, registro.QtdEstoque, registro.Descricao, registro.Status);
    }
}
