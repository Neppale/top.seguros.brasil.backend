public static class IncidentController
{
  public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString)
  {
    app.MapGet("/ocorrencia/", [Authorize] (int? pageNumber, int? size) =>
    {
      return GetAllIncidentService.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);
    })
    .WithName("Selecionar todas as ocorrências");

    app.MapGet("/ocorrencia/cliente/{id:int}", [Authorize] (int id, int? pageNumber, int? size) =>
    {
      return GetIncidentByClientService.Get(id_cliente: id, connectionString: connectionString, pageNumber: pageNumber, size: size);
    })
    .WithName("Selecionar todas as ocorrências por cliente");

    app.MapGet("/ocorrencia/{id:int}", [Authorize] (int id) =>
    {
      return GetIncidentByIdService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar ocorrência específica");

    app.MapGet("/ocorrencia/documento/{id:int}", [Authorize] (int id) =>
    {
      return GetIncidentDocumentService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar documento de ocorrência específica");

    app.MapPost("/ocorrencia/documento/{id:int}", [Authorize] (int id, HttpRequest sentFile) =>
    {
      return InsertIncidentDocumentService.Insert(id: id, request: sentFile, connectionString: connectionString);
    })
    .WithName("Inserir documento em ocorrência");

    app.MapPost("/ocorrencia/", [Authorize] (Ocorrencia ocorrencia) =>
    {
      return InsertIncidentService.Insert(ocorrencia: ocorrencia, connectionString: connectionString);
    })
    .WithName("Inserir ocorrência");

    app.MapPut("/ocorrencia/{id:int}", [Authorize] (int id, Ocorrencia ocorrencia) =>
    {
      return UpdateIncidentService.Update(id: id, ocorrencia: ocorrencia, connectionString: connectionString);
    })
    .WithName("Alterar ocorrência específica");
  }
}
