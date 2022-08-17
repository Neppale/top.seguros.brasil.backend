static class InsertUserRepository
{
  public static int Insert(Usuario user, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("INSERT INTO Usuarios (nome_completo, email, senha, tipo, status) VALUES (@Nome, @Email, @Senha, @Tipo, @Status)", new { Nome = user.nome_completo, Email = user.email, Senha = user.senha, Tipo = user.tipo, Status = user.status });

      // Pegando o ID do usuário que acabou de ser inserido.
      int createdUsuarioId = connectionString.QueryFirstOrDefault<int>("SELECT id_usuario FROM Usuarios WHERE email = @Email", new { Email = user.email });

      return createdUsuarioId;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}