static class GetVehiclesByClientRepository
{
  public static async Task<PaginatedVehiclesByClient> Get(int id, SqlConnection connectionString, int? pageNumber, int? size)
  {
    if (size == null) size = 5;

    var vehicles = (await connectionString.QueryAsync<GetVehicleByClientDto>("SELECT id_veiculo, marca, modelo, ano, uso, placa FROM Veiculos WHERE id_cliente = @Id AND status = 'true' ORDER BY id_veiculo DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { Id = id, PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
    var vehicleCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Veiculos WHERE id_cliente = @Id AND status = 'true'", new { Id = id });
    var totalPages = (int)Math.Ceiling((double)vehicleCount / (int)size);

    return new PaginatedVehiclesByClient(vehicles, totalPages);
  }
}