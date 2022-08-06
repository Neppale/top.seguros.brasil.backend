static class GetOneUserRepository
{
  public static dynamic Get(int id, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<Usuario>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE id_Usuario = @Id", new { Id = id });
  }
}