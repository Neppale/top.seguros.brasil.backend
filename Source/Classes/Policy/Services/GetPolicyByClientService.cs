static class GetPolicyByClientService
{
  public static IResult Get(int id_cliente, int? pageNumber, SqlConnection connectionString)
  {
    if (pageNumber == null) pageNumber = 1;

    var policies = GetPolicyByClientRepository.Get(id: id_cliente, connectionString: connectionString, pageNumber: pageNumber);
    if (policies.Count() == 0) return Results.NotFound("Nenhuma apólice encontrada.");

    return Results.Ok(policies);
  }
}
