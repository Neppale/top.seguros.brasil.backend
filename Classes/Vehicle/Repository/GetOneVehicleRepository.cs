static class GetOneVehicleRepository
{
  public static Veiculo Get(int id, SqlConnection connectionString)
  {
    var vehicle = connectionString.QueryFirstOrDefault<Veiculo>("SELECT * FROM Veiculos WHERE id_Veiculo = @Id", new { Id = id });
    return vehicle;

  }
}