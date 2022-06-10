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



#region [DEBUG ENDPOINTS]


// app.MapGet("/cliente/debug", () =>
// {
//   Cliente data = new Cliente(
//       fullName: "Gabriel Verissimo Dias",
//       email: "gabriel@verissimo.dev",
//       password: "96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e",
//       cpf: "000.000.000 - 00",
//       cnh: "56739225646",
//       cep: "02867-070",
//       birthdate: DateFormatter.FormatDate(2003, 07, 22),
//       phone1: "(11)94019-3706",
//       phone2: null,
//       status: true);
//   Console.WriteLine("[INFO] A request for 'cliente' was made. The response was a mock. :)");
//   return data;
// })
// .WithName("cliente/debug");

// app.MapGet("/usuario/debug", () =>
// {
//   Usuario data = new Usuario(
//     fullName: "Jefferson Oliveira Silva",
//     email: "jefferson.silva@topseguros.br",
//     password: "96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e",
//     type: "Corretor",
//     status: true);

//   Console.WriteLine("[INFO] A request for 'usuario' was made. The response was a mock. :)");
//   return data;
// })
// .WithName("usuario/debug");

// app.MapGet("/cobertura/debug", () =>
// {
//   Cobertura data = new Cobertura(
//     name: "Essencial",
//     description: "Menos dor de cabeca",
//     price: 23.90,
//     status: true);

//   Console.WriteLine("[INFO] A request for 'cobertura' was made. The response was a mock. :)");
//   return data;
// })
// .WithName("cobertura/debug");

// app.MapGet("/terceirizado/debug", () =>
// {
//   Terceirizado data = new Terceirizado(
//       fullName: "Evandro Dias",
//       function: "Guincheiro",
//       price: 100.00,
//       cnpj: "72.223.762/0001-68",
//       phone: "(11)98738-4468",
//       status: true);

//   Console.WriteLine("[INFO] A request for 'terceirizado' was made. The response was a mock. :)");
//   return data;
// })
// .WithName("terceirizado/debug");

// app.MapGet("/veiculo/debug", () =>
// {
//   Veiculo data = new Veiculo(brand: "Mazda",
//     model: "RX 7 2.6 Turbo",
//     year: 1993,
//     usage: "Passeio",
//     plate: "NDD-9961",
//     renavam: "93696427753",
//     sinistrado: false,
//     idcliente: 1);

//   Console.WriteLine("[INFO] A request for 'veiculo' was made. The response was a mock. :)");
//   return data;
// })
// .WithName("veiculo/debug");

// app.MapGet("/ocorrencia/debug", () =>
// {
//   Ocorrencia data = new Ocorrencia(
//     date: DateFormatter.FormatDate(2022, 05, 03),
//     place: "Av. Paulista, 900",
//     UF: "SP",
//     city: "Sao Paulo",
//     description: "Assalto de veiculo a mao armada",
//     type: "Assalto",
//     document: null,
//     status: "Analise",
//     idterceirizado: null,
//     idcliente: 1,
//     idveiculo: 1);

//   Console.WriteLine("[INFO] A request for 'ocorrencia' was made. The response was a mock. :)");
//   return data;
// })
// .WithName("ocorrencia/debug");

// app.MapGet("/apolice/debug", () =>
// {
//   Apolice data = new Apolice(
//     startDate: DateFormatter.FormatDate(2022, 01, 01),
//     endDate: DateFormatter.FormatDate(2023, 01, 01),
//     premium: 130.00,
//     indemnity: 52452.00,
//     idcobertura: 1,
//     idusuario: 1,
//     idcliente: 1,
//     idveiculo: 1,
//     status: "Aprovado");

//   Console.WriteLine("[INFO] A request for 'apolice' was made. The response was a mock. :)");
//   return data;
// })
// .WithName("apolice/debug");



#endregion



#region [ACTUAL ENDPOINTS]
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

