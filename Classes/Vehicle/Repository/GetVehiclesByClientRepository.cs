static class GetVehiclesByClientRepository
{
  public static IEnumerable<dynamic> Get(int id, SqlConnection connectionString, int? pageNumber)
  {
    var vehicles = connectionString.Query<dynamic>("SELECT id_veiculo, marca, modelo, ano, uso, placa from Veiculos WHERE id_cliente = @Id ORDER BY id_veiculo OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { @Id = id, @PageNumber = (pageNumber - 1) * 5 });

    return vehicles;
  }
}