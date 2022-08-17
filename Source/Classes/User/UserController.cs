public static class UserController
{
  public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString, WebApplicationBuilder builder)
  {
    app.MapGet("/usuario/", [Authorize] (int? pageNumber) =>
    {
      return GetAllUserService.Get(connectionString: connectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todos os usuários");

    app.MapGet("/usuario/{id:int}", [Authorize] (int id) =>
    {
      return GetOneUserService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar usuário específico");

    app.MapPost("/usuario/", [Authorize] (Usuario usuario) =>
    {
      return InsertUserService.Insert(usuario: usuario, connectionString: connectionString);
    })
    .WithName("Inserir usuário");

    app.MapPut("/usuario/{id:int}", [Authorize] (int id, Usuario usuario) =>
    {
      return UpdateUserService.Update(id: id, usuario: usuario, connectionString: connectionString);
    })
    .WithName("Alterar usuário específico");

    app.MapDelete("/usuario/{id:int}", [Authorize] (int id) =>
    {
      return DeleteUserService.Delete(id: id, connectionString: connectionString);
    })
    .WithName("Desativar usuário específico");

    app.MapPost("/usuario/login", [AllowAnonymous] (Usuario usuario) =>
    {
      return LoginUserService.Login(email: usuario.email, password: usuario.senha, connectionString: connectionString, builder: builder);
    })
    .WithName("Fazer login do usuário");
  }
}
