static class GetAllUserRepository
{
  public static async Task<PaginatedUsers> Get(SqlConnection connectionString, int? pageNumber, int? size, string? search)
  {
    if (size == null) size = 5;

    GetUserDto[] users;
    var totalPages = 0;

    if (search != null)
    {
      users = (await connectionString.QueryAsync<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE nome_completo LIKE @Search AND status = 'true' OR email LIKE @Search AND status = 'true' ORDER BY id_usuario DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size, Search = $"%{search}%" })).ToArray();
      var userCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Usuarios WHERE nome_completo LIKE @Search AND status = 'true' OR email LIKE @Search AND status = 'true'", new { Search = $"%{search}%" });
      totalPages = (int)Math.Ceiling((double)userCount / (int)size);
    }
    else
    {
      users = (await connectionString.QueryAsync<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE status = 'true' ORDER BY id_usuario DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
      var userCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Usuarios WHERE nome_completo LIKE @Search AND status = 'true' OR email LIKE @Search AND status = 'true'", new { Search = $"%{search}%" });
      totalPages = (int)Math.Ceiling((double)userCount / (int)size);
    }

    return new PaginatedUsers(users, totalPages);
  }
}