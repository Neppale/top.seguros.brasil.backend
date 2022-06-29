public static class TerceirizadoController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/terceirizado/", [Authorize] (int? pageNumber) =>
    {
      return GetAllTerceirizadoService.Get(dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todos os terceirizados");

    app.MapGet("/terceirizado/{id:int}", [Authorize] (int id) =>
    {
      return GetOneTerceirizadoService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar terceirizado específico");

    app.MapPost("/terceirizado/", [Authorize] (Terceirizado terceirizado) =>
    {
      return InsertTerceirizadoService.Insert(terceirizado: terceirizado, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir terceirizado");

    app.MapPut("/terceirizado/{id:int}", [Authorize] (int id, Terceirizado terceirizado) =>
    {
      return UpdateTerceirizadoService.Update(id: id, terceirizado: terceirizado, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar terceirizado específico");

    app.MapDelete("/terceirizado/{id:int}", [Authorize] (int id) =>
    {
      return DeleteTerceirizadoService.Delete(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Desativar terceirizado específico");
  }

}
