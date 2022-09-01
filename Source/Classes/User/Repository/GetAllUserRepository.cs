static class GetAllUserRepository
{
  public static IEnumerable<GetUserDto> Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    return connectionString.Query<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status from Usuarios WHERE status = 'true' ORDER BY id_usuario OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
  }
}