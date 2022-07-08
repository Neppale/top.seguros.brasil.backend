public static class ClientController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString, WebApplicationBuilder builder)
  {
    app.MapGet("/cliente/", [Authorize] (int? pageNumber) =>
    {
      return GetAllClientService.Get(dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todos os clientes");

    app.MapGet("/cliente/{id:int}", [Authorize] (int id) =>
    {
      return GetOneClientService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar cliente específico");

    app.MapPost("/cliente/", [AllowAnonymous] (Cliente cliente) =>
    {
      return InsertClientService.Insert(cliente: cliente, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir cliente");

    app.MapPut("/cliente/{id:int}", [Authorize] (int id, Cliente cliente) =>
    {
      return UpdateClientService.Update(id: id, cliente: cliente, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar cliente específico");

    app.MapDelete("/cliente/{id:int}", [Authorize] (int id) =>
    {
      return DeleteClientService.Delete(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Desativar cliente específico");

    app.MapPost("/cliente/login", [AllowAnonymous] (Cliente cliente) =>
    {
      return LoginClientService.Login(email: cliente.email, password: cliente.senha, dbConnectionString: dbConnectionString, builder: builder);
    })
    .WithName("Fazer login do cliente");
  }
}