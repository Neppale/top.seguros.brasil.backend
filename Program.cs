using tsb.mininal.policy.engine.Utils;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;

#region [CONFIGURATION]
var builder = WebApplication.CreateBuilder(args);

// Adiciona servicos ao container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Version = "v1",
    Title = "Top Seguros Brasil Policy Engine",
    Description = "Motor de gerenciamento de apolices para Top Seguros Brasil. Feito com amor <3",
  }); ;
});

var app = builder.Build();
string dbConnectionString = builder.Configuration["dbConnectionStringDev"];

// Configura a pipeline de requests HTTP.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection(); // Automaticamente redireciona requests feitos em HTTP para HTTPS

SqlConnection connectionString = new SqlConnection(dbConnectionString);
connectionString.Open();

#endregion


// HEALTHCHECK
app.MapGet("/healthcheck/", () =>
{
  return Healthcheck.Check(dbConnectionString: dbConnectionString);
})
.WithName("Checar saúde do banco de dados");

// CLIENTES 
ClienteController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

// APOLICES 
ApoliceController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

// COBERTURAS
CoberturaController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

// OCORRENCIAS
OcorrenciaController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

// TERCEIRIZADOS
TerceirizadoController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

// USUARIOS
UsuarioController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

// VEICULOS
VeiculoController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

app.Run();

