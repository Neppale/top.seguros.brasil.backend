public static class CoberturaController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/cobertura/", [Authorize] (int? pageNumber) =>
    {
      return GetAllCoberturaService.Get(dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todas as coberturas");

    app.MapGet("/cobertura/{id:int}", [Authorize] (int id) =>
    {
      return GetOneCoberturaService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar cobertura específica");

    app.MapPost("/cobertura/", [Authorize] (Cobertura cobertura) =>
    {
      return InsertCoberturaService.Insert(cobertura: cobertura, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir cobertura");

    app.MapPut("/cobertura/{id:int}", [Authorize] (int id, Cobertura cobertura) =>
    {
      return UpdateCoberturaService.Update(id: id, cobertura: cobertura, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar cobertura específica");

    app.MapDelete("/cobertura/{id:int}", [Authorize] (int id) =>
    {
      return DeleteCoberturaService.Delete(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Desativar cobertura específica");
  }
}
