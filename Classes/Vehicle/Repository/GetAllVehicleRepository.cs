static class GetAllVehicleRepository
{
  public static IEnumerable<Veiculo> Get(SqlConnection connectionString, int? pageNumber)
  {
    var vehicles = connectionString.Query<Veiculo>("SELECT id_veiculo, marca, modelo, Clientes.nome_completo AS dono, placa FROM Veiculos LEFT JOIN Clientes ON Clientes.id_cliente = Veiculos.id_cliente WHERE Veiculos.status = 'true' ORDER BY id_veiculo OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });
    return vehicles;
  }
}