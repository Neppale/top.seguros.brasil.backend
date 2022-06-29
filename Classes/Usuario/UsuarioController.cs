public static class UsuarioController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString, WebApplicationBuilder builder)
  {
    app.MapGet("/usuario/", [Authorize] (int? pageNumber) =>
    {
      return GetAllUsuarioService.Get(dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todos os usuários");

    app.MapGet("/usuario/{id:int}", [Authorize] (int id) =>
    {
      return GetOneUsuarioService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar usuário específico");

    app.MapPost("/usuario/", [Authorize] (Usuario usuario) =>
    {
      return InsertUsuarioService.Insert(usuario: usuario, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir usuário");

    app.MapPut("/usuario/{id:int}", [Authorize] (int id, Usuario usuario) =>
    {
      return UpdateUsuarioService.Update(id: id, usuario: usuario, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar usuário específico");

    app.MapDelete("/usuario/{id:int}", [Authorize] (int id) =>
    {
      return DeleteUsuarioService.Delete(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Desativar usuário específico");

    app.MapPost("/usuario/login", [AllowAnonymous] (Usuario usuario) =>
    {
      return LoginUsuarioService.Login(email: usuario.email, password: usuario.senha, dbConnectionString: dbConnectionString, builder: builder);
    })
    .WithName("Fazer login do usuário");
  }
}
