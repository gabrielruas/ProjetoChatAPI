## Pré-requisitos

- Instalar o **SDK do .NET 9**, disponível no [site oficial da Microsoft](https://dotnet.microsoft.com/download/dotnet/9.0).
- Configurar a conexão com o banco de dados no projeto da API.
- Ter a ferramenta `dotnet-ef` disponível para executar as migrations.

## Estrutura do projeto

O projeto está organizado em camadas, cada uma com uma responsabilidade:

- **API:** recebe as requisições do frontend, encaminha para a camada Aplicacao e retorna as respostas.
- **Aplicacao:** coordena as operações e validações, utilizando o Repositorio para consultar e salvar dados.
- **Repositorio:** realiza as consultas, inclusões, alterações e exclusões no banco de dados.
- **Dominio:** contém as entidades e as regras de negócio do sistema. As entidades representam os dados que podem ser mapeados para tabelas no banco.
- **Services:** reúne serviços auxiliares e integrações com sistemas externos.

## Como executar o projeto

Execute os comandos a partir da pasta da solução.

> Os exemplos consideram as pastas `Repositorio` e `API`. Ajuste os caminhos conforme os nomes utilizados no projeto.

### 1. Restaurar as dependências

```bash
dotnet restore
```

Esse comando baixa as dependências necessárias para compilar o projeto.

### 2. Criar a migration inicial

Acesse a pasta da camada Repositorio:

```bash
cd ProjetoChatRepositorio
```

Se o projeto ainda não possuir migrations, execute:

```bash
dotnet tool install --global dotnet-ef --version "9.*"

```
```bash
dotnet ef migrations add Inicio 

```

Esse comando gera os arquivos que descrevem a estrutura do banco de dados, com base nas entidades e nas configurações do contexto.


### 3. Atualizar o banco de dados

Ainda na pasta Repositorio, execute:

```bash
dotnet ef database update 
```

Esse comando aplica as migrations ao banco de dados configurado, criando ou atualizando sua estrutura.

### 4. Compilar a solução

Retorne à pasta da solução e execute:

```bash
cd ..
dotnet build
```

Esse comando compila os projetos e verifica se existem erros de compilação.

### 5. Iniciar a APIc

Acesse a pasta da API e execute:

```bash
cd API
dotnet run
```

Após a inicialização, o terminal exibirá o endereço em que a API está disponível. Mantenha o terminal aberto enquanto estiver utilizando a aplicação.