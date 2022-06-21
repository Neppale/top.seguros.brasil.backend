public static class ApoliceController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/apolice/", () =>
    {
      return GetAllApoliceService.Get(dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar todas as apólices");

    app.MapGet("/apolice/{id:int}", (int id) =>
    {
      return GetOneApoliceService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar apólice específica");

    app.MapPost("/apolice/", (Apolice apolice) =>
    {
      return InsertApoliceService.Insert(apolice: apolice, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir apólice");

    app.MapPut("/apolice/{id:int}", (int id, ApoliceStatus status) =>
    {
      return UpdateStatusApoliceService.UpdateStatus(id: id, status: status, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar status de apólice específica");
  }
}