public static class IncidentController
{
  public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString)
  {
    app.MapGet("/ocorrencia/", [Authorize] async (int? pageNumber, int? size) =>
    {
      return await GetAllIncidentService.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);
    })
    .WithName("Selecionar todas as ocorrências");

    app.MapGet("/ocorrencia/cliente/{id:int}", [Authorize] async (int id, int? pageNumber, int? size) =>
    {
      return await GetIncidentByClientService.Get(id_cliente: id, connectionString: connectionString, pageNumber: pageNumber, size: size);
    })
    .WithName("Selecionar todas as ocorrências por cliente");

    app.MapGet("/ocorrencia/{id:int}", [Authorize] async (int id) =>
    {
      return await GetIncidentByIdService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar ocorrência específica");

    app.MapGet("/ocorrencia/documento/{id:int}", [Authorize] async (int id) =>
    {
      return await GetIncidentDocumentService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar documento de ocorrência específica");

    app.MapPost("/ocorrencia/documento/{id:int}", [Authorize] async (int id, HttpRequest sentFile) =>
    {
      return await InsertIncidentDocumentService.Insert(id: id, request: sentFile, connectionString: connectionString);
    })
    .WithName("Inserir documento em ocorrência");

    app.MapPost("/ocorrencia/", [Authorize] async (Ocorrencia ocorrencia) =>
    {
      return await InsertIncidentService.Insert(ocorrencia: ocorrencia, connectionString: connectionString);
    })
    .WithName("Inserir ocorrência");

    app.MapPut("/ocorrencia/{id:int}", [Authorize] async (int id, Ocorrencia ocorrencia) =>
    {
      return await UpdateIncidentService.Update(id: id, ocorrencia: ocorrencia, connectionString: connectionString);
    })
    .WithName("Alterar ocorrência específica");
  }
}
