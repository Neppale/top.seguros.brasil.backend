static class UpdatePolicyStatusRepository
{
  public static int Update(int id, string status, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Apolices SET status = @Status WHERE id_apolice = @Id", new { Id = id, Status = status });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}