static class GetUserByIdRepository
{
  public static async Task<GetUserDto> Get(int id, SqlConnection connectionString)
  {
    return await connectionString.QueryFirstOrDefaultAsync<GetUserDto>("SELECT id_usuario, nome_completo, email, tipo, status FROM Usuarios WHERE id_Usuario = @Id AND status = 'true'", new { Id = id });
  }
}