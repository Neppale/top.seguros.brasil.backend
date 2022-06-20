using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;

static class UpdateUsuarioService
{
  /** <summary> Esta função altera um Usuario no banco de dados. </summary>**/
  public static IResult Update(int id, Usuario usuario, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do Usuario é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(usuario);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    bool isExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_usuario FROM Usuarios WHERE id_usuario = @Id", new { Id = id });
    if (!isExistent) return Results.NotFound("Usuário não encontrado.");

    // Criptografando a senha do usuário.
    usuario.senha = PasswordHasher.HashPassword(usuario.senha);

    try
    {
      connectionString.Query("UPDATE Usuarios SET nome_completo = @Nome, email = @Email, senha = @Senha, tipo = @Tipo, status = @Status WHERE id_Usuario = @Id", new { Nome = usuario.nome_completo, Email = usuario.email, Senha = usuario.senha, Tipo = usuario.tipo, Status = usuario.status, Id = id });

      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}