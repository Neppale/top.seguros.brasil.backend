public static class ClientController
{
  public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString, IDictionary<string, string> environmentVariables)
  {
    app.MapGet("/cliente/", [Authorize] (int? pageNumber, int? size) =>
    {
      return GetAllClientService.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);
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

    app.MapPost("/cliente/login", [AllowAnonymous] (ClientLoginDto login) =>
    {
      return LoginClientService.Login(login: login, connectionString: connectionString, environmentVariables: environmentVariables);
    })
    .WithName("Fazer login do cliente");
  }
}