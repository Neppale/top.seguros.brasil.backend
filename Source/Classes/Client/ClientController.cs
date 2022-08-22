public static class ClientController
{
  public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString, WebApplicationBuilder builder)
  {
    app.MapGet("/cliente/", [Authorize] (int? pageNumber) =>
    {
      return GetAllClientService.Get(connectionString: connectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todos os clientes");

    app.MapGet("/cliente/{id:int}", [Authorize] (int id) =>
    {
      return GetClientByIdService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar cliente específico");

    app.MapPost("/cliente/", [AllowAnonymous] (Cliente cliente) =>
    {
      return InsertClientService.Insert(cliente: cliente, connectionString: connectionString);
    })
    .WithName("Inserir cliente");

    app.MapPut("/cliente/{id:int}", [Authorize] (int id, Cliente cliente) =>
    {
      return UpdateClientService.Update(id: id, cliente: cliente, connectionString: connectionString);
    })
    .WithName("Alterar cliente específico");

    app.MapDelete("/cliente/{id:int}", [Authorize] (int id) =>
    {
      return DeleteClientService.Delete(id: id, connectionString: connectionString);
    })
    .WithName("Desativar cliente específico");

    app.MapPost("/cliente/login", [AllowAnonymous] (Cliente cliente) =>
    {
      return LoginClientService.Login(email: cliente.email, password: cliente.senha, connectionString: connectionString, builder: builder);
    })
    .WithName("Fazer login do cliente");
  }
}