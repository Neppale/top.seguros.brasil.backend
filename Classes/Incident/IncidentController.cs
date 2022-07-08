public static class IncidentController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/ocorrencia/", [Authorize] (int? pageNumber) =>
    {
      return GetAllIncidentService.Get(dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todas as ocorrências");

    app.MapGet("/ocorrencia/cliente/{id:int}", [Authorize] (int id, int? pageNumber) =>
    {
      return GetAllIncidentsByClientService.Get(id_cliente: id, dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todas as ocorrências por cliente");

    app.MapGet("/ocorrencia/{id:int}", [Authorize] (int id) =>
    {
      return GetOneIncidentService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar ocorrência específica");

    app.MapGet("/ocorrencia/documento/{id:int}", [Authorize] (int id) =>
    {
      return GetIncidentDocumentService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar documento de ocorrência específica");

    app.MapPost("/ocorrencia/documento/{id:int}", [Authorize] (int id, HttpRequest sentFile) =>
    {
      return InsertIncidentDocumentService.Insert(id: id, request: sentFile, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir documento em ocorrência");

    app.MapPost("/ocorrencia/", [Authorize] (Ocorrencia ocorrencia) =>
    {
      return InsertIncidentService.Insert(ocorrencia: ocorrencia, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir ocorrência");

    app.MapPut("/ocorrencia/{id:int}", [Authorize] (int id, Ocorrencia ocorrencia) =>
    {
      return UpdateIncidentService.Update(id: id, ocorrencia: ocorrencia, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar ocorrência específica");
  }
}
