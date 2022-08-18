static class UpdateUserRepository
{
  public static GetUserDto? Update(int id, Usuario usuario, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Usuarios SET nome_completo = @Nome, email = @Email, senha = @Senha, tipo = @Tipo WHERE id_Usuario = @Id", new { Nome = usuario.nome_completo, Email = usuario.email, Senha = usuario.senha, Tipo = usuario.tipo, Id = id });

      var updatedUser = connectionString.QueryFirstOrDefault<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE id_Usuario = @Id", new { Id = id });
      return updatedUser;
    }
    catch (SystemException)
    {
      return null;
    }
  }
}