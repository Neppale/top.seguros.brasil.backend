public abstract class UsuarioController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/usuario/", () =>
    {
      return GetAllUsuarioService.Get(dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar todos os usuários");

    app.MapGet("/usuario/{id:int}", (int id) =>
    {
      return GetOneUsuarioService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar usuário específico");

    app.MapPost("/usuario/", (Usuario usuario) =>
    {
      return InsertUsuarioService.Insert(usuario: usuario, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir usuário");

    app.MapPut("/usuario/{id:int}", (int id, Usuario usuario) =>
    {
      return UpdateUsuarioService.Update(id: id, usuario: usuario, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar usuário específico");

    app.MapDelete("/usuario/{id:int}", (int id) =>
    {
      return DeleteUsuarioService.Delete(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Desativar usuário específico");

    app.MapPost("/usuario/login", (Usuario usuario) =>
    {
      return LoginUsuarioService.Login(email: usuario.email, password: usuario.senha, dbConnectionString: dbConnectionString);
    })
    .WithName("Fazer login do usuário");
  }
}
