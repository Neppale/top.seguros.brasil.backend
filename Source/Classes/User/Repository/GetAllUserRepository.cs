static class GetAllUserRepository
{
    public static async Task<IEnumerable<GetUserDto>> Get(SqlConnection connectionString, int? pageNumber, int? size, string? search)
    {
        if (search != null) return await connectionString.QueryAsync<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE nome_completo LIKE @Search AND status = 'true' OR email LIKE @Search AND status = 'true' ORDER BY id_usuario DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size, Search = $"%{search}%" });
        else return await connectionString.QueryAsync<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE status = 'true' ORDER BY id_usuario DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
    }
}