static class GetAllPolicyRepository
{
    public static async Task<PaginatedPolicies> Get(SqlConnection connectionString, int? pageNumber, int? size)
    {
        if (size == null) size = 5;

        var policies = (await connectionString.QueryAsync<GetPolicyDto>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE status != 'Rejeitada' ORDER BY id_apolice DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
        var policyCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Apolices WHERE status != 'Rejeitada'");
        var totalPages = (int)Math.Ceiling((double)policyCount / (double)size);

        foreach (var policy in policies)
        {
            policy.data_inicio = SqlDateConverter.ConvertToShow(policy.data_inicio);
            policy.data_fim = SqlDateConverter.ConvertToShow(policy.data_fim);
        }

        return new PaginatedPolicies(policies: policies, totalPages: totalPages);
    }
}