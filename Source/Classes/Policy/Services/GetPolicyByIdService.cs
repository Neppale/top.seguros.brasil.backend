static class GetPolicyByIdService
{
    /** <summary> Esta função retorna uma apólice específica no banco de dados. </summary>**/
    public static async Task<IResult> Get(int id, SqlConnection connectionString)
    {
        var policy = await GetPolicyByIdRepository.Get(id, connectionString);
        if (policy == null) return Results.NotFound(new { message = "Apólice não encontrada." });

        policy.data_inicio = SqlDateConverter.Convert(policy.data_inicio);
        policy.data_fim = SqlDateConverter.Convert(policy.data_fim);

        var enrichedPolicy = await PolicyEnrichment.Enrich(policy, connectionString);

        return Results.Ok(enrichedPolicy);
    }
}

