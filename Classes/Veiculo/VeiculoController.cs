public abstract class VeiculoController
{
  // GET: Veiculo
  public static IResult Handle(string method, string dbConnectionString)
  {
    switch (method)
    {
      case "GETALL":
        return GetAllVeiculoService.Get(dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString)
  {
    switch (method)
    {
      case "GETONE":
        return GetOneVeiculoService.Get(id: id, dbConnectionString: dbConnectionString);
      case "DELETE":
        return DeleteVeiculoService.Delete(id: id, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, string dbConnectionString, Veiculo receivedData)
  {
    switch (method)
    {
      case "POST":
        return InsertVeiculoService.Insert(veiculo: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString, Veiculo receivedData)
  {
    switch (method)
    {
      case "PUT":
        return UpdateVeiculoService.Update(id: id, veiculo: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
}
