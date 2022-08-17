static class GetClientHashPasswordByEmailRepository
{
  public static string Get(string email, SqlConnection connectionString)
  {
    var password = connectionString.QueryFirstOrDefault<string>("SELECT senha FROM Clientes WHERE email = @Email AND status = 'true'", new { Email = email });

    return password;
  }
}


