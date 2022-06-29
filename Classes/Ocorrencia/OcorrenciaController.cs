public static class OcorrenciaController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/ocorrencia/", [Authorize] (int? pageNumber) =>
    {
      return GetAllOcorrenciaService.Get(dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todas as ocorrências");

    app.MapGet("/ocorrencia/cliente/{id:int}", [Authorize] (int id, int? pageNumber) =>
    {
      return GetAllOcorrenciasByClienteService.Get(id_cliente: id, dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todas as ocorrências por cliente");

    app.MapGet("/ocorrencia/{id:int}", [Authorize] (int id) =>
    {
      return GetOneOcorrenciaService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar ocorrência específica");

    app.MapGet("/ocorrencia/documento/{id:int}", [Authorize] (int id) =>
    {
      return GetDocumentOcorrenciaService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar documento de ocorrência específica");

    app.MapPost("/ocorrencia/documento/{id:int}", [Authorize] (int id, HttpRequest sentFile) =>
    {
      return InsertDocumentOcorrenciaService.Insert(id: id, request: sentFile, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir documento em ocorrência");

    app.MapPost("/ocorrencia/", [Authorize] (Ocorrencia ocorrencia) =>
    {
      return InsertOcorrenciaService.Insert(ocorrencia: ocorrencia, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir ocorrência");

    app.MapPut("/ocorrencia/{id:int}", [Authorize] (int id, Ocorrencia ocorrencia) =>
    {
      return UpdateOcorrenciaService.Update(id: id, ocorrencia: ocorrencia, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar ocorrência específica");
  }
}
