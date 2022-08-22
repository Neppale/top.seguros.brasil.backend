static class GetUserByIdRepository
{
  public static GetUserDto Get(int id, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE id_Usuario = @Id AND status = 'true'", new { Id = id });
  }
}