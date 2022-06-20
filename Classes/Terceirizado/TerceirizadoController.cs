public abstract class TerceirizadoController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/terceirizado/", () =>
    {
      return GetAllTerceirizadoService.Get(dbConnectionString: dbConnectionString);

    })
    .WithName("Selecionar todos os terceirizados");

    app.MapGet("/terceirizado/{id:int}", (int id) =>
    {
      return GetOneTerceirizadoService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar terceirizado específico");

    app.MapPost("/terceirizado/", (Terceirizado terceirizado) =>
    {
      return InsertTerceirizadoService.Insert(terceirizado: terceirizado, dbConnectionString: dbConnectionString);

    })
    .WithName("Inserir terceirizado");

    app.MapPut("/terceirizado/{id:int}", (int id, Terceirizado terceirizado) =>
    {
      return UpdateTerceirizadoService.Update(id: id, terceirizado: terceirizado, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar terceirizado específico");

  }

}
