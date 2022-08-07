static class GetUserByEmailRepository
{
  public static dynamic Get(string email, SqlConnection connectionString)
  {
    var user = connectionString.QueryFirstOrDefault("SELECT id_usuario, nome_completo, email, tipo FROM Usuarios WHERE email = @Email", new { Email = email });
    return user;
  }
}