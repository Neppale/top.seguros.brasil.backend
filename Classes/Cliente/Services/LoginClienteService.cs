using tsb.mininal.policy.engine.Utils;
using Dapper;
using Microsoft.Data.SqlClient;

/** <summary> Esta função faz o login do cliente. </summary>**/
abstract class LoginClienteService
{
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