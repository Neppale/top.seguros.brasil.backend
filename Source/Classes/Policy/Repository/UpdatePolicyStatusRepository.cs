static class UpdatePolicyStatusRepository
{
  public static async Task<int> Update(int id, string status, SqlConnection connectionString)
  {
    try
    {
      await connectionString.QueryAsync("UPDATE Apolices SET status = @Status WHERE id_apolice = @Id", new { Id = id, Status = status });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}