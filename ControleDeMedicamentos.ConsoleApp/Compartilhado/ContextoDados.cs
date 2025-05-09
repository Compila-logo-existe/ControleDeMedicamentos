﻿using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public class ContextoDados
{
    public List<Fornecedor> Fornecedores { get; set; }
    public List<Funcionario> Funcionarios { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
    public List<RequisicaoEntrada> RequisicoesEntrada { get; set; }
    public List<RequisicaoSaida> RequisicoesSaida { get; set; }
    public List<Paciente> Pacientes { get; set; }
    public List<PrescricaoMedica> PrescricoesMedicas { get; set; }
    public List<PrescricaoMedicamento> MedicamentosPrescricoes { get; set; }

    private string pastaArmazenamento = string.Empty;
    private string arquivoArmazenamento = "dados-controle-medicamento.json";

    public ContextoDados()
    {
        Fornecedores = new List<Fornecedor>();
        Funcionarios = new List<Funcionario>();
        Medicamentos = new List<Medicamento>();
        RequisicoesEntrada = new List<RequisicaoEntrada>();
        RequisicoesSaida = new List<RequisicaoSaida>();
        Pacientes = new List<Paciente>();
        PrescricoesMedicas = new List<PrescricaoMedica>();
        MedicamentosPrescricoes = new List<PrescricaoMedicamento>();

    }

    public void VerificarSistemaOperacional()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            pastaArmazenamento = @"C:\temp";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            pastaArmazenamento = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "temp");
        }
    }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            Carregar();
    }

    public void Salvar()
    {
        VerificarSistemaOperacional();
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(pastaArmazenamento))
            Directory.CreateDirectory(pastaArmazenamento);

        File.WriteAllText(caminhoCompleto, json);
    }

    public void Carregar()
    {
        VerificarSistemaOperacional();
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        if (!File.Exists(caminhoCompleto)) return;

        string json = File.ReadAllText(caminhoCompleto);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(json, jsonOptions)!;

        if (contextoArmazenado == null) return;

        Fornecedores = contextoArmazenado.Fornecedores;
        Funcionarios = contextoArmazenado.Funcionarios;
        Medicamentos = contextoArmazenado.Medicamentos;
        RequisicoesEntrada = contextoArmazenado.RequisicoesEntrada;
        RequisicoesSaida = contextoArmazenado.RequisicoesSaida;
        Pacientes = contextoArmazenado.Pacientes;
        PrescricoesMedicas = contextoArmazenado.PrescricoesMedicas;
        MedicamentosPrescricoes = contextoArmazenado.MedicamentosPrescricoes;
    }
}
