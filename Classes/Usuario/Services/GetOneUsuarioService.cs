using Dapper;
using Microsoft.Data.SqlClient;

static class GetOneUsuarioService
{
  /** <summary> Esta função retorna um usuário específico no banco de dados. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault("SELECT id_usuario, nome_completo, email, tipo, status from Usuarios WHERE id_Usuario = @Id", new { Id = id });

    if (data == null) return Results.NotFound("Usuário não encontrado.");

    return Results.Ok(data);
  }
}