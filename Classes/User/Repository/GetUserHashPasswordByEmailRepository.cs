static class GetUserHashPasswordByEmailRepository
{
  public static string Get(string email, SqlConnection connectionString)
  {
    var password = connectionString.QueryFirstOrDefault<string>("SELECT senha FROM Usuarios WHERE email = @Email AND status = 'true'", new { Email = email });

    return password;
  }
}