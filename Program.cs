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

#region [ENDPOINTS]
// HEALTHCHECK

app.MapGet("/healthcheck/", () =>
{
  return HealthCheck.Check(dbConnectionString: dbConnectionString);
})
.WithName("Checar saúde do banco de dados");


// CLIENTES 

app.MapGet("/cliente/", () =>
{
  return ClienteController.Handle(method: "GETALL", dbConnectionString: dbConnectionString);
})
.WithName("Selecionar todos os clientes");

app.MapGet("/cliente/{id:int}", (int id) =>
{
  return ClienteController.Handle(method: "GETONE", id: id, dbConnectionString: dbConnectionString);
})
.WithName("Selecionar cliente específico");

app.MapPost("/cliente/", (Cliente cliente) =>
{
  return ClienteController.Handle(method: "POST", dbConnectionString: dbConnectionString, receivedData: cliente);
})
.WithName("Inserir cliente");

app.MapPut("/cliente/{id:int}", (int id, Cliente cliente) =>
{
  return ClienteController.Handle(method: "PUT", id: id, dbConnectionString: dbConnectionString, receivedData: cliente);
})
.WithName("Alterar cliente específico");

app.MapPost("/cliente/login", (Cliente cliente) =>
{
  return ClienteController.Handle(method: "LOGIN", dbConnectionString: dbConnectionString, receivedData: cliente);
})
.WithName("Fazer login do cliente");

// APOLICES 

app.MapGet("/apolice/", () =>
{
  return ApoliceController.Handle(method: "GETALL", dbConnectionString: dbConnectionString);

})
.WithName("Selecionar todas as apólices");

app.MapGet("/apolice/{id:int}", (int id) =>
{
  return ApoliceController.Handle(method: "GETONE", id: id, dbConnectionString: dbConnectionString);
})
.WithName("Selecionar apólice específica");

app.MapPost("/apolice/", (Apolice apolice) =>
{
  return ApoliceController.Handle(method: "POST", dbConnectionString: dbConnectionString, receivedData: apolice);
})
.WithName("Inserir apólice");

app.MapPut("/apolice/{id:int}", (int id, Apolice apolice) =>
{
  return ApoliceController.Handle(method: "PUT", id: id, dbConnectionString: dbConnectionString, receivedData: apolice);
})
.WithName("Alterar status de apólice específica");

// COBERTURAS

app.MapGet("/cobertura/", () =>
{
  return CoberturaController.Handle(method: "GETALL", dbConnectionString: dbConnectionString);
})
.WithName("Selecionar todas as coberturas");

app.MapGet("/cobertura/{id:int}", (int id) =>
{
  return CoberturaController.Handle(method: "GETONE", id: id, dbConnectionString: dbConnectionString);
})
.WithName("Selecionar cobertura específica");

app.MapPost("/cobertura/", (Cobertura cobertura) =>
{
  return CoberturaController.Handle(method: "POST", receivedData: cobertura, dbConnectionString: dbConnectionString);

})
.WithName("Inserir cobertura");

app.MapPut("/cobertura/{id:int}", (int id, Cobertura cobertura) =>
{
  return CoberturaController.Handle(method: "PUT", receivedData: cobertura, id: id, dbConnectionString: dbConnectionString);
})
.WithName("Alterar cobertura específica");

// OCORRENCIAS

app.MapGet("/ocorrencia/", () =>
{
  return OcorrenciaController.Handle(method: "GETALL", dbConnectionString: dbConnectionString);
})
.WithName("Selecionar todas as ocorrências");

app.MapGet("/ocorrencia/{id:int}", (int id) =>
{
  return OcorrenciaController.Handle(method: "GETONE", id: id, dbConnectionString: dbConnectionString);
})
.WithName("Selecionar ocorrência específica");

app.MapGet("/ocorrencia/documento/{id:int}", (int id) =>
{
  return OcorrenciaController.Handle(method: "GETDOCUMENT", id: id, dbConnectionString: dbConnectionString);
})
.WithName("Selecionar documento de ocorrência específica");

app.MapPost("/ocorrencia/documento/{id:int}", (int id, HttpRequest sentFile) =>
{
  return OcorrenciaController.Handle(method: "POSTDOCUMENT", id: id, dbConnectionString: dbConnectionString, request: sentFile);
})
.WithName("Inserir documento em ocorrência");

app.MapPost("/ocorrencia/", (Ocorrencia ocorrencia) =>
{
  return OcorrenciaController.Handle(method: "POST", receivedData: ocorrencia, dbConnectionString: dbConnectionString);
})
.WithName("Inserir ocorrência");

app.MapPut("/ocorrencia/{id:int}", (int id, Ocorrencia ocorrencia) =>
{
  return OcorrenciaController.Handle(method: "PUT", receivedData: ocorrencia, dbConnectionString: dbConnectionString);
})
.WithName("Inserir ocorrência");

// TERCEIRIZADOS

app.MapGet("/terceirizado/", () =>
{
  return TerceirizadoController.Handle(method: "GETALL", dbConnectionString: dbConnectionString);

})
.WithName("Selecionar todos os terceirizados");

app.MapGet("/terceirizado/{id:int}", (int id) =>
{
  return TerceirizadoController.Handle(method: "GETONE", id: id, dbConnectionString: dbConnectionString);
})
.WithName("Selecionar terceirizado específico");

app.MapPost("/terceirizado/", (Terceirizado terceirizado) =>
{
  return TerceirizadoController.Handle(method: "POST", receivedData: terceirizado, dbConnectionString: dbConnectionString);

})
.WithName("Inserir terceirizado");

app.MapPut("/terceirizado/{id:int}", (int id, Terceirizado terceirizado) =>
{
  return TerceirizadoController.Handle(method: "PUT", receivedData: terceirizado, id: id, dbConnectionString: dbConnectionString);
})
.WithName("Alterar terceirizado específico");

// USUARIOS

app.MapGet("/usuario/", () =>
{
  return UsuarioController.Handle(method: "GETALL", dbConnectionString: dbConnectionString);
})
.WithName("Selecionar todos os usuários");

app.MapGet("/usuario/{id:int}", (int id) =>
{
  return UsuarioController.Handle(method: "GETONE", id: id, dbConnectionString: dbConnectionString);
})
.WithName("Selecionar usuário específico");

app.MapPost("/usuario/", (Usuario usuario) =>
{
  return UsuarioController.Handle(method: "POST", receivedData: usuario, dbConnectionString: dbConnectionString);
})
.WithName("Inserir usuário");

app.MapPut("/usuario/{id:int}", (int id, Usuario usuario) =>
{
  return UsuarioController.Handle(method: "PUT", receivedData: usuario, id: id, dbConnectionString: dbConnectionString);
})
.WithName("Alterar usuário específico");

app.MapPost("/usuario/login", (Usuario usuario) =>
{
  return UsuarioController.Handle(method: "LOGIN", receivedData: usuario, dbConnectionString: dbConnectionString);
})
.WithName("Fazer login do usuário");

// VEICULOS

app.MapGet("/veiculo/", () =>
{
  return VeiculoController.Handle(method: "GETALL", dbConnectionString: dbConnectionString);

})
.WithName("Selecionar todos os veículos");

app.MapGet("/veiculo/{id:int}", (int id) =>
{
  return VeiculoController.Handle(method: "GETONE", id: id, dbConnectionString: dbConnectionString);
})
.WithName("Selecionar veículo específico");

app.MapPost("/veiculo/", (Veiculo veiculo) =>
{
  return VeiculoController.Handle(method: "POST", receivedData: veiculo, dbConnectionString: dbConnectionString);

})
.WithName("Inserir veículo");

app.MapPut("/veiculo/{id:int}", (int id, Veiculo veiculo) =>
{
  return VeiculoController.Handle(method: "PUT", receivedData: veiculo, id: id, dbConnectionString: dbConnectionString);
})
.WithName("Alterar veículo específico");

#endregion

app.Run();

