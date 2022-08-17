static class GetAllUserRepository
{
  public static IEnumerable<dynamic> Get(SqlConnection connectionString, int? pageNumber)
  {
    var users = connectionString.Query<dynamic>("SELECT id_usuario, nome_completo, email, tipo from Usuarios WHERE status = 'true' ORDER BY id_usuario OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });

    return users;
  }
}