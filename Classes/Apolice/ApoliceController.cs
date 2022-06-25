public static class ApoliceController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/apolice/", [Authorize] () =>
    {
      return GetAllApoliceService.Get(dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar todas as apólices");

    app.MapGet("/apolice/{id:int}", [Authorize] (int id) =>
    {
      return GetOneApoliceService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar apólice específica");

    app.MapPost("/apolice/", [Authorize] (Apolice apolice) =>
    {
      return InsertApoliceService.Insert(apolice: apolice, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir apólice");

    app.MapPut("/apolice/{id:int}/{status:alpha}", [Authorize] (int id, string status) =>
    {
      return UpdateStatusApoliceService.UpdateStatus(id: id, status: status, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar status de apólice específica");

    app.MapPost("/apolice/gerar/", [Authorize] (GenerateApolice apolice) =>
    {
      return GenerateApoliceService.Generate(id_veiculo: apolice.id_veiculo, id_cliente: apolice.id_cliente, id_cobertura: apolice.id_cobertura, dbConnectionString: dbConnectionString);
    })
    .WithName("Gerar apólice");
  }
}