using EscolaDeCursos.WebApp.Compartilhado.Apresentacao;
using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura;
using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do container de injeção de dependência
builder.Services.AddInfraRepositories(builder.Configuration);
builder.Services.AddPresentationConfig();

var app = builder.Build();

// Aplica as migrations pendentes antes de atender às requisições.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EscolaDeCursosDbContext>();
    dbContext.Database.Migrate();
}

// Middlewares de roteamento
app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();
