static class GetAllUsuarioService
{
  /** <summary> Esta função retorna todos os usuários no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query("SELECT id_usuario, nome_completo, email, tipo from Usuarios WHERE status = 'true'");

    return Results.Ok(data);
  }
}