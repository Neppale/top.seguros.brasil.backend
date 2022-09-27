static class GetVehiclesByClientRepository
{
    public static async Task<IEnumerable<GetVehicleByClientDto>> Get(int id, SqlConnection connectionString, int? pageNumber, int? size)
    {
        return await connectionString.QueryAsync<GetVehicleByClientDto>("SELECT id_veiculo, marca, modelo, ano, uso, placa from Veiculos WHERE id_cliente = @Id AND status = 'true' ORDER BY id_veiculo DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { @Id = id, @PageNumber = (pageNumber - 1) * size, Size = size });
    }
}