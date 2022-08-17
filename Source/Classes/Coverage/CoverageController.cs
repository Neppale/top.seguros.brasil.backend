public static class CoverageController
{
  public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString)
  {
    app.MapGet("/cobertura/", [Authorize] (int? pageNumber) =>
    {
      return GetAllCoverageService.Get(connectionString: connectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todas as coberturas");

    app.MapGet("/cobertura/{id:int}", [Authorize] (int id) =>
    {
      return GetOneCoverageService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar cobertura específica");

    app.MapPost("/cobertura/", [Authorize] (Cobertura cobertura) =>
    {
      return InsertCoverageService.Insert(cobertura: cobertura, connectionString: connectionString);
    })
    .WithName("Inserir cobertura");

    app.MapPut("/cobertura/{id:int}", [Authorize] (int id, Cobertura cobertura) =>
    {
      return UpdateCoverageService.Update(id: id, cobertura: cobertura, connectionString: connectionString);
    })
    .WithName("Alterar cobertura específica");

    app.MapDelete("/cobertura/{id:int}", [Authorize] (int id) =>
    {
      return DeleteCoverageService.Delete(id: id, connectionString: connectionString);
    })
    .WithName("Desativar cobertura específica");
  }
}
