static class GetAllVehicleRepository
{
  public static IEnumerable<GetAllVehicleDto> Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    return connectionString.Query<GetAllVehicleDto>("SELECT id_veiculo, marca, modelo, Clientes.nome_completo AS dono, placa FROM Veiculos LEFT JOIN Clientes ON Clientes.id_cliente = Veiculos.id_cliente WHERE Veiculos.status = 'true' ORDER BY id_veiculo OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
  }
}