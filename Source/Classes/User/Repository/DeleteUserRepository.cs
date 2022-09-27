static class DeleteUserRepository
{
  public static async Task<int> Delete(int id, SqlConnection connectionString)
  {
    try
    {
      await connectionString.QueryAsync("UPDATE Usuarios SET status = 'false' WHERE id_Usuario = @Id", new { Id = id });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}