public static class UserController
{
    public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString, WebApplicationBuilder builder)
    {
        app.MapGet("/usuario/", [Authorize] async (int? pageNumber, int? size, string? search) =>
        {
            return await GetAllUserService.Get(connectionString: connectionString, pageNumber: pageNumber, size: size, search: search);
        })
        .WithName("Selecionar todos os usuários");

        app.MapGet("/usuario/{id:int}", [Authorize] async (int id) =>
        {
            return await GetUserByIdService.Get(id: id, connectionString: connectionString);
        })
        .WithName("Selecionar usuário específico");

        app.MapPost("/usuario/", [Authorize] async (Usuario usuario) =>
        {
            return await InsertUserService.Insert(usuario: usuario, connectionString: connectionString);
        })
        .WithName("Inserir usuário");

        app.MapPut("/usuario/{id:int}", [Authorize] async (int id, Usuario usuario) =>
        {
            return await UpdateUserService.Update(id: id, usuario: usuario, connectionString: connectionString);
        })
        .WithName("Alterar usuário específico");

        app.MapDelete("/usuario/{id:int}", [Authorize] async (int id) =>
        {
            return await DeleteUserService.Delete(id: id, connectionString: connectionString);
        })
        .WithName("Desativar usuário específico");

        app.MapPost("/usuario/login", [AllowAnonymous] async (UserLoginDto login) =>
        {
            return await LoginUserService.Login(login: login, connectionString: connectionString, builder: builder);
        })
        .WithName("Fazer login do usuário");
    }
}
