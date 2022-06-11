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
  return cliente.Get(dbConnectionString: dbConnectionString);
})
.WithName("/cliente/");

app.MapGet("/cliente/{id:int}", (int id) =>
{
  Cliente cliente = new();
  return cliente.Get(id: id, dbConnectionString: dbConnectionString);
})
.WithName("/cliente/{id:int}");

app.MapPost("/cliente/", (Cliente cliente) =>
{
  return cliente.Insert(cliente: cliente, dbConnectionString: dbConnectionString);
})
.WithName("POST /cliente/");

app.MapPut("/cliente/{id:int}", (int id, Cliente cliente) =>
{
  return cliente.Update(id, cliente, dbConnectionString);
})
.WithName("PUT /cliente/");

// APOLICES 

app.MapGet("/apolice/", () =>
{
  Apolice apolice = new();
  return apolice.Get(dbConnectionString: dbConnectionString);

})
.WithName("/apolice/");

app.MapGet("/apolice/{id:int}", (int id) =>
{
  Apolice apolice = new();
  return apolice.Get(id: id, dbConnectionString: dbConnectionString);
})
.WithName("/apolice/{id:int}");

app.MapPost("/apolice/", (Apolice apolice) =>
{
  return apolice.Insert(apolice: apolice, dbConnectionString: dbConnectionString);
})
.WithName("POST /apolice/");

app.MapPut("/apolice/{id:int}", (int id, Apolice apolice) =>
{
  return apolice.Update(id: id, apolice: apolice, dbConnectionString: dbConnectionString);
})
.WithName("PUT /apolice/");

// COBERTURAS

app.MapGet("/cobertura/", () =>
{
  Cobertura cobertura = new();
  return cobertura.Get(dbConnectionString: dbConnectionString);

})
.WithName("/cobertura/");

app.MapGet("/cobertura/{id:int}", (int id) =>
{
  Cobertura cobertura = new();
  return cobertura.Get(id: id, dbConnectionString: dbConnectionString);
})
.WithName("/cobertura/{id:int}");

app.MapPost("/cobertura/", (Cobertura cobertura) =>
{
  return cobertura.Insert(cobertura: cobertura, dbConnectionString: dbConnectionString);

})
.WithName("POST /cobertura/");

app.MapPut("/cobertura/{id:int}", (int id, Cobertura cobertura) =>
{
  return cobertura.Update(id: id, cobertura: cobertura, dbConnectionString: dbConnectionString);
})
.WithName("PUT /cobertura/");

// OCORRENCIAS

app.MapGet("/ocorrencia/", () =>
{
  Ocorrencia ocorrencia = new();
  return ocorrencia.Get(dbConnectionString: dbConnectionString);

})
.WithName("/ocorrencia/");

app.MapGet("/ocorrencia/{id:int}", (int id) =>
{
  Ocorrencia ocorrencia = new();
  return ocorrencia.Get(id: id, dbConnectionString: dbConnectionString);

})
.WithName("/ocorrencia/{id:int}");

// TERCEIRIZADOS

app.MapGet("/terceirizado/", () =>
{
  Terceirizado terceirizado = new();
  return terceirizado.Get(dbConnectionString: dbConnectionString);

})
.WithName("/terceirizado/");

app.MapGet("/terceirizado/{id:int}", (int id) =>
{
  Terceirizado terceirizado = new();
  return terceirizado.Get(id: id, dbConnectionString: dbConnectionString);
})
.WithName("/terceirizado/{id:int}");

app.MapPost("/terceirizado/", (Terceirizado terceirizado) =>
{
  return terceirizado.Insert(terceirizado: terceirizado, dbConnectionString: dbConnectionString);

})
.WithName("POST /terceirizado/");

app.MapPut("/terceirizado/{id:int}", (int id, Terceirizado terceirizado) =>
{
  return terceirizado.Update(id: id, terceirizado: terceirizado, dbConnectionString: dbConnectionString);
})
.WithName("PUT /terceirizado/");

// USUARIOS

app.MapGet("/usuario/", () =>
{
  Usuario usuario = new();
  return usuario.Get(dbConnectionString: dbConnectionString);
})
.WithName("/usuario/");

app.MapGet("/usuario/{id:int}", (int id) =>
{
  Usuario usuario = new();
  return usuario.Get(id: id, dbConnectionString: dbConnectionString);

})
.WithName("/usuario/{id:int}");

app.MapPost("/usuario/", (Usuario usuario) =>
{
  return usuario.Insert(usuario: usuario, dbConnectionString: dbConnectionString);

})
.WithName("POST /usuario/");

app.MapPut("/usuario/{id:int}", (int id, Usuario usuario) =>
{
  return usuario.Update(id: id, usuario: usuario, dbConnectionString: dbConnectionString);
})
.WithName("PUT /usuario/");

// VEICULOS

app.MapGet("/veiculo/", () =>
{
  Veiculo veiculo = new();
  return veiculo.Get(dbConnectionString: dbConnectionString);

})
.WithName("/veiculo/");

app.MapGet("/veiculo/{id:int}", (int id) =>
{
  Veiculo veiculo = new();
  return veiculo.Get(id: id, dbConnectionString: dbConnectionString);
})
.WithName("/veiculo/{id:int}");

app.MapPost("/veiculo/", (Veiculo veiculo) =>
{
  return veiculo.Insert(veiculo: veiculo, dbConnectionString: dbConnectionString);

})
.WithName("POST /veiculo/");

app.MapPut("/veiculo/{id:int}", (int id, Veiculo veiculo) =>
{
  return veiculo.Update(id: id, veiculo: veiculo, dbConnectionString: dbConnectionString);
})
.WithName("PUT /veiculo/");

#endregion

app.Run();

