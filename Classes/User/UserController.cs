public static class UserController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString, WebApplicationBuilder builder)
  {
    app.MapGet("/usuario/", [Authorize] (int? pageNumber) =>
    {
      return GetAllUserService.Get(dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todos os usuários");

    app.MapGet("/usuario/{id:int}", [Authorize] (int id) =>
    {
      return GetOneUserService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar usuário específico");

    app.MapPost("/usuario/", [Authorize] (Usuario usuario) =>
    {
      return InsertUserService.Insert(usuario: usuario, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir usuário");

    app.MapPut("/usuario/{id:int}", [Authorize] (int id, Usuario usuario) =>
    {
      return UpdateUserService.Update(id: id, usuario: usuario, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar usuário específico");

    app.MapDelete("/usuario/{id:int}", [Authorize] (int id) =>
    {
      return DeleteUserService.Delete(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Desativar usuário específico");

    app.MapPost("/usuario/login", [AllowAnonymous] (Usuario usuario) =>
    {
      return LoginUserService.Login(email: usuario.email, password: usuario.senha, dbConnectionString: dbConnectionString, builder: builder);
    })
    .WithName("Fazer login do usuário");
  }
}
