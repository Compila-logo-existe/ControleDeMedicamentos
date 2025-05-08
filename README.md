# 💉​Controle de Medicamentos💉​

![](https://hs.toledoprudente.edu.br/hs-fs/hubfs/contador%20profissional%20presidente%20prudente.gif?width=480&name=contador%20profissional%20presidente%20prudente.gif)

## Introdução

Este projeto consiste em um sistema completo de controle de medicamentos, desenvolvido para gerenciar o fluxo de entrada e saída de remédios em uma unidade de saúde. O programa foi criado com o objetivo de oferecer um ambiente funcional para registrar, controlar e consultar dados essenciais relacionados a fornecedores, pacientes, medicamentos, funcionários, prescrições médicas e requisições.

A aplicação permite manter o controle rigoroso dos medicamentos em estoque, evitar perdas por falta de monitoramento e garantir que a distribuição dos remédios seja feita conforme a disponibilidade e prescrições médicas válidas.
## Propriedades do programa
O sistema é dividido em sete módulos principais, cada um com suas respectivas funcionalidades e regras de negócio:

### 📦 *Módulo de Fornecedores*
- Cadastro, edição, visualização e exclusão de fornecedores.
- Validações de nome, telefone e CNPJ.
- Impede o cadastro de fornecedores com CNPJ duplicado.
<br><br>

### ​🤒 *Módulo de Pacientes*
- Cadastro completo de pacientes com dados de contato e Cartão do SUS.
- Evita duplicidade no Cartão do SUS.
- Edição, exclusão e visualização dos registros.
<br><br>

### 💊​ *Módulo de Medicamentos*
- Registro detalhado com nome, descrição, fornecedor e quantidade.
- Medicamentos com menos de 20 unidades são destacados como “em falta”.
- Se o medicamento já estiver cadastrado, o sistema atualiza a quantidade automaticamente.
- Controle completo de estoque.
<br><br>

### 👩‍💼​ *Módulo de Funcionários*
- Cadastro e gerenciamento de funcionários autorizados.
- Validação de CPF e telefone.
- Bloqueia registros duplicados por CPF.
<br><br>

### 📝 *Módulo de Prescrições Médicas*
- Cadastro de prescrições com CRM, lista de medicamentos, dosagem e período.
- Validação de disponibilidade dos medicamentos no estoque.
- Bloqueia requisições que excedem a dose prescrita.
<br><br>

### 📤 *Módulo de Requisições de Saída*
- Vincula a retirada de medicamentos a uma prescrição válida.
- Atualiza automaticamente o estoque após a saída.
- Alerta quando a retirada faz com que o medicamento entre em estado de "em falta".
<br><br>

### 📥 *Módulo de Requisições de Entrada*
- Permite registrar novas entradas de medicamentos no estoque.
- Atualização automática da quantidade em estoque.
- Valida a presença de funcionário responsável e quantidade positiva.
***
![Controle de Medicamentos](https://github.com/user-attachments/assets/7af7a4ac-a79e-4a8a-96e6-b8195804ebcb)
***
## Tecnologias

[![Tecnologias](https://skillicons.dev/icons?i=git,github,visualstudio,neovim,cs,dotnet)](https://skillicons.dev)

## Ausensias

![](https://i.pinimg.com/originals/6f/01/b7/6f01b75b69b25384b44fca0f8f099881.gif) 

- **Gif**: Ainda não foi inserido o gif do projeto funcionando
- **Desafios**: Nenhum dos desafios propostos foi realizado.
<br><br>

## Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

```
dotnet restore
```

4. Em seguida, compile a solução utilizando o comando:
   
```
dotnet build --configuration Release
```

5. Para executar o projeto compilando em tempo real
   
```
dotnet run --project ControleDeMedicamentos.ConsoleApp
```

6. Para executar o arquivo compilado, navegue até a pasta `.\ControleDeMedicamentos.ConsoleApp\bin\Release\net8.0\` e execute o arquivo:
   
```
ControleDeMedicamentos.ConsoleApp.exe
```

## Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.

# Ajude o próximo!
![](https://comb.io/RHfoT5.gif) 
