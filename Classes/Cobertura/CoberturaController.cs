public abstract class CoberturaController
{
  // GET: Cobertura
  public static IResult Handle(string method, string dbConnectionString)
  {
    switch (method)
    {
      case "GETALL":
        return GetAllCoberturaService.Get(dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString)
  {
    switch (method)
    {
      case "GETONE":
        return GetOneCoberturaService.Get(id: id, dbConnectionString: dbConnectionString);
      // case "DELETE":
      //     return DeleteCoberturaService(id: id, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, string dbConnectionString, Cobertura receivedData)
  {
    switch (method)
    {
      case "POST":
        return InsertCoberturaService.Insert(cobertura: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString, Cobertura receivedData)
  {
    switch (method)
    {
      case "PUT":
        return UpdateCoberturaService.Update(id: id, cobertura: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
}
