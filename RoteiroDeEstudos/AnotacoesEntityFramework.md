# Entity Framework e CRUD

## Instalação dos pacotes necessários para trabalhar com o EF

1. Na linha de comando do terminal digite os comandos abaixo para instalação dos pacotes.
    - Ferramenta para executar comandos do EF no console, ela é executada apenas uma vez. Não sendo necessário executar para outros projetos.
        -> dotnet tool install --global dotnet-ef

    - Pacote do EF. Esse comando deve ser executado em cada projeto criado
        -> dotnet add package Microsoft.EntityFrameworkCore.Design

    - Pacote do EF do SqlServer. Esse comando deve ser executado em cada projeto criado
        -> dotnet add package Microsoft.EntityFrameworkCore.SqlServer

2. Conferir a instalação no arquivo csproj conforme imagem abaixo.

![Modulos instalados](image.png)

## Criando a classe entidade

1. Criar uma nova pasta chamada Entities onde serão criada as classes que serão transformada em tabelas do banco de dados

2. Criar a classe Contatos.cs dentro da pasta Entities.

```csharp
    public class Contatos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        
    }
```
## Criando o Contexto

    Contexto é uma classe que centraliza todas as informações em um determinado banco de dados

    1. Criar uma pasta chamada Context
    2. Criar uma classe chamada AgendaContext.cs
    3. Criar uma herança da classe DbContext para a AgendaContext
    4. Criar o contrustor da classe que vai receber a configuração do banco de dados
    5. Criar a propriedade DbSet<> que vai representar a tabela

```csharp
using Microsoft.EntityFrameworkCore;
using MODULOAPI.Entities;

namespace MODULOAPI.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {

        }
        public DbSet<Contatos> Contatos { get; set; }
    }
}
```
## Configurando a conexão

1. Cadastrar a conexão com o banco no arquivo appsettings.Development.json. Na ConnectionString vai informações sobre o servidor, o nome do banco e o tipo de autenticação.

```Json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "ConexaoPadrao": "Server=localhost\\sqlexpress; Initial Catalog=Agenda; Integrated Security=True"
  }
}
```
2. Configurar o context passando a string de conexão.
Na classe Program.cs adicionar o código abaixo
Adiconar os usings
```csharp
using ModuloAPI.Context;
using Microsoft.EntityFrameworkCore;
```

3. Abaixo de var builder no começo do código colocar a instrução abaixo.
```csharp
builder.Services.AddDbContext<AgendaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

