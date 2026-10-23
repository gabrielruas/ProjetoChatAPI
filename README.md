## Pré-requisito

Instalar o Net 9 Dotnet  

o projeto trabalha em camadas onde e dividido em camada 
## Estrutura do projeto

- **API:** recebe as requisições do frontend, encaminha para a camada de aplicação e retorna as respostas.
- **Aplicacao:** recebe as solicitações da API, coordena as regras e validações e utiliza o Repositorio para consultar e salvar dados.
- **Repositorio:** realiza as consultas, inclusões, alterações e exclusões no banco de dados.
- **Dominio:** contém as entidades "Tabelas" e as regras de negócio que representam o funcionamento do sistema.
- **Services:** reúne serviços auxiliares e integrações com sistemas externos.

## Como executar o projeto



### 1. Criar a migration

Acesse a pasta da camada Repositorio:

```bash
cd Repositorio
```

Se o projeto ainda não possuir migrations, crie a migration inicial:

```bash
dotnet ef migrations add Inicio --startup-project ../API
```

Esse comando gera os arquivos que descrevem a estrutura do banco de dados com base nas entidades e nas configurações do contexto.

> Caso o projeto já possua migrations, pule esta etapa.

### 2. Atualizar o banco de dados

```bash
dotnet ef database update --startup-project ../API
```

Esse comando aplica as migrations, criando ou atualizando as tabelas no banco de dados configurado.

### 3. Compilar o projeto

Retorne à pasta da solução e compile:

```bash
cd ..
dotnet build
```

O comando verifica se o projeto compila corretamente. Ele não inicia a aplicação.

### 4. Iniciar a API

Acesse a pasta da API e execute:

```bash
cd API
dotnet run
```

Após a inicialização, o terminal exibirá o endereço em que a API está disponível.

