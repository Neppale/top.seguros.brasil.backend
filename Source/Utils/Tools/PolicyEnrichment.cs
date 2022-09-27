static class PolicyEnrichment
{
    public static async Task<EnrichedPolicy> Enrich(GetPolicyDto policy, SqlConnection connectionString)
    {
        EnrichedPolicy enrichedPolicy = new EnrichedPolicy();
        enrichedPolicy.id_apolice = policy.id_apolice;
        enrichedPolicy.data_inicio = policy.data_inicio;
        enrichedPolicy.data_fim = policy.data_fim;
        enrichedPolicy.premio = policy.premio;
        enrichedPolicy.indenizacao = policy.indenizacao;
        enrichedPolicy.cobertura = await GetCoverageByIdRepository.Get(policy.id_cobertura, connectionString);
        enrichedPolicy.usuario = await GetUserByIdRepository.Get(policy.id_usuario, connectionString);
        enrichedPolicy.cliente = await GetClientByIdRepository.Get(policy.id_cliente, connectionString);
        enrichedPolicy.veiculo = await GetVehicleByIdRepository.Get(policy.id_veiculo, connectionString);
        enrichedPolicy.status = policy.status;

        return enrichedPolicy;
    }
}