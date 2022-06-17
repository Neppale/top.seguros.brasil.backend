public abstract class OcorrenciaController
{
  public static IResult Handle(string method, string dbConnectionString)
  {
    switch (method)
    {
      case "GETALL":
        return GetAllOcorrenciaService.Get(dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString)
  {
    switch (method)
    {
      case "GETONE":
        return GetOneOcorrenciaService.Get(id: id, dbConnectionString: dbConnectionString);
      case "GETDOCUMENT":
        return GetDocumentOcorrenciaService.Get(id: id, dbConnectionString: dbConnectionString);
      // case "DELETE":
      //     return DeleteOcorrenciaService(id: id, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, string dbConnectionString, Ocorrencia receivedData)
  {
    switch (method)
    {
      case "POST":
        return InsertOcorrenciaService.Insert(ocorrencia: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static Task<IResult> Handle(string method, string dbConnectionString, int id, HttpRequest request)
  {
    switch (method)
    {
      case "POST":
        return InsertDocumentOcorrenciaService.Insert(request: request, id: id, dbConnectionString: dbConnectionString);
      default:
        return Task<IResult>.FromResult(Results.StatusCode(405));
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString, Ocorrencia receivedData)
  {
    switch (method)
    {
      case "PUT":
        return UpdateOcorrenciaService.Update(id: id, ocorrencia: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
}
