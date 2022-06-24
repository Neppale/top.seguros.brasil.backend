static class LoginClienteService
{
  /** <summary> Esta função faz o login do cliente. </summary>**/
  public static IResult Login(string email, string password, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    try
    {
      string hashPassword = connectionString.QueryFirstOrDefault<string>("SELECT senha FROM Clientes WHERE email = @Email", new { Email = email });

      if (hashPassword == null) return Results.BadRequest("E-mail ou senha inválidos.");

      // Verificando senha do cliente.
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