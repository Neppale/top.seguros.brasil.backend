static class DeleteVehicleRepository
{
  public static async Task<int> Delete(int id, SqlConnection connectionString)
  {
    try
    {
      await connectionString.QueryAsync("UPDATE Veiculos SET status = 'false' WHERE id_veiculo = @Id", new { Id = id });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}