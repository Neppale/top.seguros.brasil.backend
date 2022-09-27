static class GetUserByEmailRepository
{
    public static async Task<GetUserDto> Get(string email, SqlConnection connectionString)
    {
        return await connectionString.QueryFirstOrDefaultAsync<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE email = @Email", new { Email = email });
    }
}