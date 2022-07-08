static class GetAllUserService
{
  /** <summary> Esta função retorna todos os usuários no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString, int? pageNumber)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    var data = connectionString.Query("SELECT id_usuario, nome_completo, email, tipo from Usuarios WHERE status = 'true' ORDER BY id_usuario OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });

    return Results.Ok(data);
  }
}