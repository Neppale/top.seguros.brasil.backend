public static class ClientController
{
  public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString, WebApplicationBuilder builder)
  {
    app.MapGet("/cliente/", [Authorize] async (int? pageNumber, int? size) =>
    {
      return await GetAllClientService.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);
    })
    .WithName("Selecionar todos os clientes");
    
    app.MapGet("/cliente/{id:int}", [Authorize] async (int id) =>
    {
      return await GetClientByIdService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar cliente específico");

    app.MapPost("/cliente/", [AllowAnonymous] async (Cliente cliente) =>
    {
      return await InsertClientService.Insert(cliente: cliente, connectionString: connectionString);
    })
    .WithName("Inserir cliente");

    app.MapPut("/cliente/{id:int}", [Authorize] async (int id, Cliente cliente) =>
    {
      return await UpdateClientService.Update(id: id, cliente: cliente, connectionString: connectionString);
    })
    .WithName("Alterar cliente específico");

    app.MapDelete("/cliente/{id:int}", [Authorize] async (int id) =>
    {
      return await DeleteClientService.Delete(id: id, connectionString: connectionString);
    })
    .WithName("Desativar cliente específico");

    app.MapPost("/cliente/login", [AllowAnonymous] async (ClientLoginDto login) =>
    {
      return await LoginClientService.Login(login: login, connectionString: connectionString, builder: builder);
    })
    .WithName("Fazer login do cliente");
  }
}