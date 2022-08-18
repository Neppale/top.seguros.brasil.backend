static class GetUserByEmailRepository
{
  public static GetUserDto Get(string email, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE email = @Email", new { Email = email });
  }
}