public abstract class TerceirizadoController
{
  // GET: Cobertura
  public static IResult Handle(string method, string dbConnectionString)
  {
    switch (method)
    {
      case "GETALL":
        return GetAllTerceirizadoService.Get(dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString)
  {
    switch (method)
    {
      case "GETONE":
        return GetOneTerceirizadoService.Get(id: id, dbConnectionString: dbConnectionString);
      // case "DELETE":
      //     return DeleteCoberturaService(id: id, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, string dbConnectionString, Terceirizado receivedData)
  {
    switch (method)
    {
      case "POST":
        return InsertTerceirizadoService.Insert(terceirizado: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString, Terceirizado receivedData)
  {
    switch (method)
    {
      case "PUT":
        return UpdateTerceirizadoService.Update(id: id, terceirizado: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
}
