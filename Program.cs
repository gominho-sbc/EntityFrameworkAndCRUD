using MODULOAPI.Context;
using Microsoft.EntityFrameworkCore;

// Inicializa o construtor da aplicação web, preparando as configurações e serviços
var builder = WebApplication.CreateBuilder(args);

// Habilita o suporte a Controllers tradicionais (classes de controle dedicadas, como o seu ContatoController)
builder.Services.AddControllers();

// Configura o Entity Framework Core para conectar-se ao banco de dados SQL Server 
// usando a string de conexão "ConexaoPadrao" definida no arquivo appsettings.json
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

// Ativa o explorador de endpoints, essencial para que o Swagger consiga rastrear e mapear as rotas da API
builder.Services.AddEndpointsApiExplorer();

// Adiciona os serviços do gerador do Swagger para criar a especificação técnica da API
builder.Services.AddSwaggerGen();

// Finaliza a fase de configuração dos serviços e constrói de fato a aplicação web
var app = builder.Build();

// Verifica se o projeto está rodando em ambiente de Desenvolvimento local
if (app.Environment.IsDevelopment())
{
    // Gera o arquivo de metadados no formato JSON interno que descreve as rotas da aplicação
    app.UseSwagger();
    
    // Habilita a interface visual colorida do Swagger no navegador (geralmente acessada em /swagger)
    app.UseSwaggerUI();
}

// Mapeia de forma automática todas as rotas declaradas nos seus arquivos de Controller ([Route("api/[controller]")])
app.MapControllers();

// Cria uma rota de teste manual e direta (Minimal API) apenas para validar se o servidor web está respondendo corretamente
app.MapGet("/weatherforecast", () => new[] { new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 25, "Warm") })
    .WithName("GetWeatherForecast")
    .WithOpenApi(); // Diz ao Swagger para incluir essa rota direta na documentação visual

// Coloca o servidor web no ar e mantém a aplicação rodando à espera de requisições HTTP
app.Run();

// Modelo simples de dados (Record) usado apenas como estrutura para a resposta da rota de teste acima
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary);
