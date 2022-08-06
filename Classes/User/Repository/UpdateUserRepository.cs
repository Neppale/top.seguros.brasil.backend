static class UpdateUserRepository
{
  public static int Update(int id, Usuario usuario, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Usuarios SET nome_completo = @Nome, email = @Email, senha = @Senha, tipo = @Tipo WHERE id_Usuario = @Id", new { Nome = usuario.nome_completo, Email = usuario.email, Senha = usuario.senha, Tipo = usuario.tipo, Id = id });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}