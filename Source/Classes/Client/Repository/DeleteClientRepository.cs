static class DeleteClientRepository
{
  public static async Task<int> Delete(int id, SqlConnection connectionString)
  {
    try
    {
      await connectionString.QueryAsync("UPDATE Clientes SET status = 'false' WHERE id_cliente = @Id", new { Id = id });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}