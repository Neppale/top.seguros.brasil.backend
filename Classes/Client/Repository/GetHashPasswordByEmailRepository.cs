static class GetHashPasswordByEmailRepository
{
  public static string Get(string email, SqlConnection connectionString)
  {
    var password = connectionString.QueryFirstOrDefault<string>("SELECT senha FROM Clientes WHERE email = @Email", new { Email = email });

    return password;
  }
}


