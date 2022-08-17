static class GetClientHashPasswordByEmailRepository
{
  public static string Get(string email, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<string>("SELECT senha FROM Clientes WHERE email = @Email AND status = 'true'", new { Email = email });
  }
}


