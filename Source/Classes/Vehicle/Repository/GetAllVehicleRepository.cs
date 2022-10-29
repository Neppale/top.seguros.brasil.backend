static class GetAllVehicleRepository
{
    public static async Task<PaginatedVehicles> Get(SqlConnection connectionString, int? pageNumber, int? size, string? search)
    {


        GetAllVehicleDto[] vehicles;
        var totalPages = 0;
        if (search != null)
        {
            vehicles = (await connectionString.QueryAsync<GetAllVehicleDto>("SELECT id_veiculo, placa, marca, modelo, ano, Clientes.nome_completo AS dono FROM Veiculos LEFT JOIN Clientes ON Clientes.id_cliente = Veiculos.id_cliente WHERE placa LIKE @Search AND Veiculos.status = 'true' OR Clientes.nome_completo LIKE @Search AND Veiculos.status = 'true' ORDER BY id_veiculo DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size, Search = $"%{search}%" })).ToArray();
            totalPages = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Veiculos LEFT JOIN Clientes ON Clientes.id_cliente = Veiculos.id_cliente WHERE placa LIKE @Search AND Veiculos.status = 'true' OR Clientes.nome_completo LIKE @Search AND Veiculos.status = 'true'", new { Search = $"%{search}%" });
        }
        else
        {
            vehicles = (await connectionString.QueryAsync<GetAllVehicleDto>("SELECT id_veiculo, placa, marca, modelo, ano, Clientes.nome_completo AS dono FROM Veiculos LEFT JOIN Clientes ON Clientes.id_cliente = Veiculos.id_cliente WHERE Veiculos.status = 'true' ORDER BY id_veiculo DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
            totalPages = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Veiculos LEFT JOIN Clientes ON Clientes.id_cliente = Veiculos.id_cliente WHERE Veiculos.status = 'true'");
        }
        return new PaginatedVehicles(vehicles, totalPages);
    }
}