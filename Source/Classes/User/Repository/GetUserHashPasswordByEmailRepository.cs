static class GetUserHashPasswordByEmailRepository
{
    public static async Task<string> Get(string email, SqlConnection connectionString)
    {
        var password = await connectionString.QueryFirstOrDefaultAsync<string>("SELECT senha FROM Usuarios WHERE email = @Email AND status = 'true'", new { Email = email });

        return password;
    }
}