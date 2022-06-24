static class LoginUsuarioService
{
  /** <summary> Esta função realiza o login do usuario. </summary>**/
  public static IResult Login(string email, string password, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    try
    {
      string hashPassword = connectionString.QueryFirstOrDefault<string>("SELECT senha FROM Usuarios WHERE email = @Email", new { Email = email });

      if (hashPassword == null) return Results.BadRequest("E-mail ou senha inválidos.");

      // Verificando senha do usuario.
      bool isValid = PasswordHasher.Verify(hashPassword, password);
      if (!isValid) return Results.BadRequest("E-mail ou senha inválidos.");

      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}