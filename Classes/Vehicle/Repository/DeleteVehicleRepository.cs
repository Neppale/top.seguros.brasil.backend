static class DeleteVehicleRepository
{
  public static int Delete(int id, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Veiculos SET status = 'false' WHERE id_veiculo = @Id", new { Id = id });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}