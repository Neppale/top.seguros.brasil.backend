static class GetUserHashPasswordByEmailService
{
  public static string Get(string email, SqlConnection connectionString)
  {
    var password = GetUserHashPasswordByEmailRepository.Get(email: email, connectionString: connectionString);
    return password;
  }
}


