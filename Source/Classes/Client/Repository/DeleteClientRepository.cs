static class DeleteClientRepository
{
  public static int Delete(int id, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Clientes SET status = 'false' WHERE id_cliente = @Id", new { Id = id });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}