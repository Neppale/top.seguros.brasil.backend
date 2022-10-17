
static class GetPolicyByUserRepository
{
    public static async Task<IEnumerable<GetPolicyByUserDto>> Get(int id, SqlConnection connectionString, int? pageNumber, int? size, string? search)
    {
        if (search != null) return await connectionString.QueryAsync<GetPolicyByUserDto>("SELECT id_apolice, Clientes.nome_completo AS nome, Coberturas.nome AS tipo, Veiculos.modelo AS veiculo, Apolices.status FROM Apolices LEFT JOIN Clientes ON Clientes.id_cliente = Apolices.id_cliente LEFT JOIN Coberturas ON Coberturas.id_cobertura = Apolices.id_cobertura LEFT JOIN Veiculos ON Veiculos.id_veiculo = Apolices.id_veiculo WHERE Apolices.id_usuario = @Id AND Clientes.nome_completo LIKE @Search OR Apolices.id_usuario = @Id AND Veiculos.modelo LIKE @Search ORDER BY id_apolice DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { Id = id, PageNumber = (pageNumber - 1) * size, Size = size, Search = $"%{search}%" });

        else return await connectionString.QueryAsync<GetPolicyByUserDto>("SELECT id_apolice, Clientes.nome_completo AS nome, Coberturas.nome AS tipo, Veiculos.modelo AS veiculo, Apolices.status FROM Apolices LEFT JOIN Clientes ON Clientes.id_cliente = Apolices.id_cliente LEFT JOIN Coberturas ON Coberturas.id_cobertura = Apolices.id_cobertura LEFT JOIN Veiculos ON Veiculos.id_veiculo = Apolices.id_veiculo WHERE Apolices.id_usuario = @Id ORDER BY id_apolice DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { Id = id, PageNumber = (pageNumber - 1) * size, Size = size });
    }
}