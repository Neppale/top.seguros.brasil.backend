
static class DeleteOutsourcedRepository
{
  public static int Delete(int id, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Terceirizados SET status = 'false' WHERE id_terceirizado = @Id", new { Id = id });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}