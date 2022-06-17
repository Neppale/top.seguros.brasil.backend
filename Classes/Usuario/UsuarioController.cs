public abstract class UsuarioController
{
  // GET: Usuario
  public static IResult Handle(string method, string dbConnectionString)
  {
    switch (method)
    {
      case "GETALL":
        return GetAllUsuarioService.Get(dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString)
  {
    switch (method)
    {
      case "GETONE":
        return GetOneUsuarioService.Get(id: id, dbConnectionString: dbConnectionString);
      case "DELETE":
        return DeleteUsuarioService.Delete(id: id, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, string dbConnectionString, Usuario receivedData)
  {
    switch (method)
    {
      case "POST":
        return InsertUsuarioService.Insert(usuario: receivedData, dbConnectionString: dbConnectionString);
      case "LOGIN":
        return LoginUsuarioService.Login(email: receivedData.email, password: receivedData.senha, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
  public static IResult Handle(string method, int id, string dbConnectionString, Usuario receivedData)
  {
    switch (method)
    {
      case "PUT":
        return UpdateUsuarioService.Update(id: id, usuario: receivedData, dbConnectionString: dbConnectionString);
      default:
        return Results.StatusCode(405);
    }
  }
}
