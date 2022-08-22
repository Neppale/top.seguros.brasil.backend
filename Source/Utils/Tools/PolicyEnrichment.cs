static class PolicyEnrichment
{
  public static EnrichedPolicy Enrich(Apolice policy, SqlConnection connectionString)
  {
    EnrichedPolicy enrichedPolicy = new EnrichedPolicy();
    enrichedPolicy.id_apolice = policy.id_apolice;
    enrichedPolicy.data_inicio = policy.data_inicio;
    enrichedPolicy.data_fim = policy.data_fim;
    enrichedPolicy.premio = policy.premio;
    enrichedPolicy.indenizacao = policy.indenizacao;
    enrichedPolicy.cobertura = GetCoverageByIdRepository.Get(policy.id_cobertura, connectionString);
    enrichedPolicy.usuario = GetUserByIdRepository.Get(policy.id_usuario, connectionString);
    enrichedPolicy.cliente = GetClientByIdRepository.Get(policy.id_cliente, connectionString);
    enrichedPolicy.veiculo = GetVehicleByIdRepository.Get(policy.id_veiculo, connectionString);
    enrichedPolicy.status = policy.status;

    return enrichedPolicy;
  }
}