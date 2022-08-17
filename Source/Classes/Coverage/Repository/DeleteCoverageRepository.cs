static class DeleteCoverageRepository
{
  public static int Delete(int id, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Coberturas SET status = 'false' WHERE id_cobertura = @Id", new { Id = id });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}