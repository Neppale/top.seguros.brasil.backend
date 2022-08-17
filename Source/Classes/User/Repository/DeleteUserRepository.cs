static class DeleteUserRepository
{
  public static int Delete(int id, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Usuarios SET status = 'false' WHERE id_Usuario = @Id", new { Id = id });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}