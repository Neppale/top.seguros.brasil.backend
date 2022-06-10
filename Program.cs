using tsb.mininal.policy.engine.Utils;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using Dapper;

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
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection(); // Automaticamente redireciona requests feitos em HTTP para HTTPS
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
SqlConnection connectionString = new SqlConnection(dbConnectionString);
connectionString.Open();

#endregion

#region [ENDPOINTS]
// HEALTHCHECK

app.MapGet("/healthcheck/", () =>
{
  return HealthCheck.Check(dbConnectionString: dbConnectionString);
})
.WithName("/healthcheck/");


// CLIENTES 

app.MapGet("/cliente/", () =>
{
  Cliente cliente = new();
  var data = cliente.Get(dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/cliente/");

app.MapGet("/cliente/{id:int}", (int id) =>
{
  Cliente cliente = new();
  var data = cliente.Get(id: id, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/cliente/{id:int}");

app.MapPost("/cliente/", (Cliente cliente) =>
{
  var data = cliente.Insert(cliente: cliente, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("POST /cliente/");

app.MapPut("/cliente/{id:int}", (int id, Cliente cliente) =>
{
  var data = cliente.Update(id, cliente, dbConnectionString);
  return data;
})
.WithName("PUT /cliente/");

// APOLICES 

app.MapGet("/apolice/", () =>
{
  Apolice apolice = new();
  var data = apolice.Get(dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/apolice/");

app.MapGet("/apolice/{id:int}", (int id) =>
{
  Apolice apolice = new();
  var data = apolice.Get(id: id, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/apolice/{id:int}");

app.MapPost("/apolice/", (Apolice apolice) =>
{
  var data = apolice.Insert(apolice: apolice, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("POST /apolice/");

app.MapPut("/apolice/", (Apolice apolice) =>
{
  return Results.StatusCode(501);
})
.WithName("PUT /apolice/");

// COBERTURAS

app.MapGet("/cobertura/", () =>
{
  Cobertura cobertura = new();
  var data = cobertura.Get(dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/cobertura/");

app.MapGet("/cobertura/{id:int}", (int id) =>
{
  Cobertura cobertura = new();
  var data = cobertura.Get(id: id, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/cobertura/{id:int}");

app.MapPost("/cobertura/", (Cobertura cobertura) =>
{
  var data = cobertura.Insert(cobertura: cobertura, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("POST /cobertura/");

app.MapPut("/cobertura/", (Cobertura cobertura) =>
{
  return Results.StatusCode(501);
})
.WithName("PUT /cobertura/");

// OCORRENCIAS

app.MapGet("/ocorrencia/", () =>
{
  Ocorrencia ocorrencia = new();
  var data = ocorrencia.Get(dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/ocorrencia/");

app.MapGet("/ocorrencia/{id:int}", (int id) =>
{
  Ocorrencia ocorrencia = new();
  var data = ocorrencia.Get(id: id, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/ocorrencia/{id:int}");

// TERCEIRIZADOS

app.MapGet("/terceirizado/", () =>
{
  Terceirizado terceirizado = new();
  var data = terceirizado.Get(dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/terceirizado/");

app.MapGet("/terceirizado/{id:int}", (int id) =>
{
  Terceirizado terceirizado = new();
  var data = terceirizado.Get(id: id, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/terceirizado/{id:int}");

app.MapPost("/terceirizado/", (Terceirizado terceirizado) =>
{
  var data = terceirizado.Insert(terceirizado: terceirizado, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("POST /terceirizado/");

app.MapPut("/terceirizado/", (Terceirizado terceirizado) =>
{
  return Results.StatusCode(501);
})
.WithName("PUT /terceirizado/");

// USUARIOS

app.MapGet("/usuario/", () =>
{
  Usuario usuario = new();
  var data = usuario.Get(dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/usuario/");

app.MapGet("/usuario/{id:int}", (int id) =>
{
  Usuario usuario = new();
  var data = usuario.Get(id: id, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/usuario/{id:int}");

app.MapPost("/usuario/", (Usuario usuario) =>
{
  var data = usuario.Insert(usuario: usuario, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("POST /usuario/");

app.MapPut("/usuario/", (Usuario usuario) =>
{
  return Results.StatusCode(501);
})
.WithName("PUT /usuario/");

// VEICULOS

app.MapGet("/veiculo/", () =>
{
  Veiculo veiculo = new();
  var data = veiculo.Get(dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/veiculo/");

app.MapGet("/veiculo/{id:int}", (int id) =>
{
  Veiculo veiculo = new();
  var data = veiculo.Get(id: id, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("/veiculo/{id:int}");

app.MapPost("/veiculo/", (Veiculo veiculo) =>
{
  var data = veiculo.Insert(veiculo: veiculo, dbConnectionString: dbConnectionString);
  return data;
})
.WithName("POST /veiculo/");

app.MapPut("/veiculo/", (Veiculo veiculo) =>
{
  return Results.StatusCode(501);
})
.WithName("PUT /veiculo/");

#endregion

app.Run();

