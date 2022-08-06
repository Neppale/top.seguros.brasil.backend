static class GetOneUserRepository
{
  public static Usuario Get(int id, SqlConnection connectionString)
  {
    var user = connectionString.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuarios WHERE id_Usuario = @Id", new { Id = id });

    return user;
  }
}