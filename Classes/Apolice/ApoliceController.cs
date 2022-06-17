public abstract class ApoliceController
{
  public static IResult Handle(string method, string dbConnectionString)
  {
    switch (method)
    {
      case "GETALL":
        return GetAllApoliceService.Get(dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString)
  {
    switch (method)
    {
      case "GETONE":
        return GetOneApoliceService.Get(id: id, dbConnectionString: dbConnectionString);
      // case "DELETE":
      //     return DeleteApoliceService(id: id, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, string dbConnectionString, Apolice receivedData)
  {
    switch (method)
    {
      case "POST":
        return InsertApoliceService.Insert(apolice: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString, Apolice receivedData)
  {
    switch (method)
    {
      case "PUT":
        return UpdateStatusApoliceService.UpdateStatus(id: id, apolice: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
}
