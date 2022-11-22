static class GetPolicyByClientService
{
  public static async Task<IResult> Get(int id_cliente, int? pageNumber, SqlConnection connectionString, int? size)
  {

    var client = await GetClientByIdRepository.Get(id: id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    if (pageNumber == null) pageNumber = 1;
    if (size == null) size = 5;

    var policies = await GetPolicyByClientRepository.Get(id: id_cliente, connectionString: connectionString, pageNumber: pageNumber, size: size);

    IEnumerable<EnrichedPolicy> enrichedPolicies = new List<EnrichedPolicy>();

    foreach (var policy in policies.policies)
    {
      var enrichedPolicy = await PolicyEnrichment.Enrich(policy: policy, connectionString: connectionString);
      enrichedPolicies = enrichedPolicies.Append(enrichedPolicy);
    }

    var enrichedPoliciesArray = enrichedPolicies.ToArray();
    var paginatedResponse = new paginatedResponse(data: enrichedPoliciesArray, totalPages: policies.totalPages);

    return Results.Ok(paginatedResponse);
  }
}
