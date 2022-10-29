static class GetPolicyByClientRepository
{
    public static async Task<PaginatedPolicies> Get(int id, SqlConnection connectionString, int? pageNumber, int? size)
    {
        if (size == null) size = 5;

        var policies = (await connectionString.QueryAsync<GetPolicyDto>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE id_cliente = @Id ORDER BY id_apolice DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { Id = id, PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
        var policyCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Apolices WHERE id_cliente = @Id", new { Id = id });
        var totalPages = (int)Math.Ceiling((double)policyCount / (double)size);

        return new PaginatedPolicies(policies: policies, totalPages: totalPages);
    }
}