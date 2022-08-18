static class InsertUserRepository
{
  public static GetUserDto? Insert(Usuario user, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("INSERT INTO Usuarios (nome_completo, email, senha, tipo, status) VALUES (@Nome, @Email, @Senha, @Tipo, @Status)", new { Nome = user.nome_completo, Email = user.email, Senha = user.senha, Tipo = user.tipo, Status = user.status });

      var createdUser = connectionString.QueryFirstOrDefault<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE email = @Email", new { Email = user.email });
      return createdUser;
    }
    catch (SystemException)
    {
      return null;
    }
  }
}