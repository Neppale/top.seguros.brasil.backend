public static class CoverageController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/cobertura/", [Authorize] (int? pageNumber) =>
    {
      return GetAllCoverageService.Get(dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todas as coberturas");

    app.MapGet("/cobertura/{id:int}", [Authorize] (int id) =>
    {
      return GetOneCoverageService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar cobertura específica");

    app.MapPost("/cobertura/", [Authorize] (Cobertura cobertura) =>
    {
      return InsertCoverageService.Insert(cobertura: cobertura, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir cobertura");

    app.MapPut("/cobertura/{id:int}", [Authorize] (int id, Cobertura cobertura) =>
    {
      return UpdateCoverageService.Update(id: id, cobertura: cobertura, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar cobertura específica");

    app.MapDelete("/cobertura/{id:int}", [Authorize] (int id) =>
    {
      return DeleteCoverageService.Delete(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Desativar cobertura específica");
  }
}
