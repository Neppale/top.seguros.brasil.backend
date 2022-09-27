static class GetVehicleByIdRepository
{
  public static async Task<Veiculo> Get(int id, SqlConnection connectionString)
  {
    return await connectionString.QueryFirstOrDefaultAsync<Veiculo>("SELECT * FROM Veiculos WHERE id_Veiculo = @Id AND status = 'true'", new { Id = id });
  }
}