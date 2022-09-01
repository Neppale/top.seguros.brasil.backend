public static class UserController
{
  public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString, WebApplicationBuilder builder)
  {
    app.MapGet("/usuario/", [Authorize] (int? pageNumber, int? size) =>
    {
      return GetAllUserService.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);
    })
    .WithName("Selecionar todos os usuários");

    app.MapGet("/usuario/{id:int}", [Authorize] (int id) =>
    {
      return GetUserByIdService.Get(id: id, connectionString: connectionString);
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

    app.MapPost("/usuario/login", [AllowAnonymous] (UserLoginDto login) =>
    {
      return LoginUserService.Login(login: login, connectionString: connectionString, builder: builder);
    })
    .WithName("Fazer login do usuário");
  }
}
