public static class ClienteController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString, WebApplicationBuilder builder)
  {
    app.MapGet("/cliente/", [Authorize] () =>
    {
      return GetAllClienteService.Get(dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar todos os clientes");

    app.MapGet("/cliente/{id:int}", [Authorize] (int id) =>
    {
      return GetOneClienteService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar cliente específico");

    app.MapPost("/cliente/", [AllowAnonymous] (Cliente cliente) =>
    {
      return InsertClienteService.Insert(cliente: cliente, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir cliente");

    app.MapPut("/cliente/{id:int}", [Authorize] (int id, Cliente cliente) =>
    {
      return UpdateClienteService.Update(id: id, cliente: cliente, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar cliente específico");

    app.MapDelete("/cliente/{id:int}", [Authorize] (int id) =>
    {
      return DeleteClienteService.Delete(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Desativar cliente específico");

    app.MapPost("/cliente/login", [AllowAnonymous] (Cliente cliente) =>
    {
      return LoginClienteService.Login(email: cliente.email, password: cliente.senha, dbConnectionString: dbConnectionString, builder: builder);
    })
    .WithName("Fazer login do cliente");
  }
}