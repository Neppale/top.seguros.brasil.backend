public abstract class ClienteController
{
  // GET: Cliente
  public static IResult Handle(string method, string dbConnectionString)
  {
    switch (method)
    {
      case "GETALL":
        return GetAllClienteService.Get(dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString)
  {
    switch (method)
    {
      case "GETONE":
        return GetOneClienteService.Get(id: id, dbConnectionString: dbConnectionString);
      // case "DELETE":
      //     return DeleteClienteService(id: id, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, string dbConnectionString, Cliente receivedData)
  {
    switch (method)
    {
      case "POST":
        return InsertClienteService.Insert(cliente: receivedData, dbConnectionString: dbConnectionString);
      case "LOGIN":
        return LoginClienteService.Login(email: receivedData.email, password: receivedData.senha, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString, Cliente receivedData)
  {
    switch (method)
    {
      case "PUT":
        return UpdateClienteService.Update(id: id, cliente: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
}
