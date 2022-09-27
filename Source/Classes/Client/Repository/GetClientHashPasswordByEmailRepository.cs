static class GetClientHashPasswordByEmailRepository
{
  public static async Task<string> Get(string email, SqlConnection connectionString)
  {
    return await connectionString.QueryFirstOrDefaultAsync<string>("SELECT senha FROM Clientes WHERE email = @Email AND status = 'true'", new { Email = email });
  }
}


