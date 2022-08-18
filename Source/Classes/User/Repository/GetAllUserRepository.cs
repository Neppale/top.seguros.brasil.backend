static class GetAllUserRepository
{
  public static IEnumerable<GetUserDto> Get(SqlConnection connectionString, int? pageNumber)
  {
    return connectionString.Query<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status from Usuarios WHERE status = 'true' ORDER BY id_usuario OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });
  }
}