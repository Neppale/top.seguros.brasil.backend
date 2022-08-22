public static class OutsourcedController
{
  public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString)
  {
    app.MapGet("/terceirizado/", [Authorize] (int? pageNumber) =>
    {
      return GetAllOutsourcedService.Get(connectionString: connectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todos os terceirizados");

    app.MapGet("/terceirizado/{id:int}", [Authorize] (int id) =>
    {
      return GetOutsourcedByIdService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar terceirizado específico");

    app.MapPost("/terceirizado/", [Authorize] (Terceirizado terceirizado) =>
    {
      return InsertOutsourcedService.Insert(terceirizado: terceirizado, connectionString: connectionString);
    })
    .WithName("Inserir terceirizado");

    app.MapPut("/terceirizado/{id:int}", [Authorize] (int id, Terceirizado terceirizado) =>
    {
      return UpdateOutsourcedService.Update(id: id, terceirizado: terceirizado, connectionString: connectionString);
    })
    .WithName("Alterar terceirizado específico");

    app.MapDelete("/terceirizado/{id:int}", [Authorize] (int id) =>
    {
      return DeleteOutsourcedService.Delete(id: id, connectionString: connectionString);
    })
    .WithName("Desativar terceirizado específico");
  }

}
