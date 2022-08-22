static class GetPolicyByClientService
{
  public static IResult Get(int id_cliente, int? pageNumber, SqlConnection connectionString)
  {

    var client = GetClientByIdRepository.Get(id: id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    if (pageNumber == null) pageNumber = 1;
    var policies = GetPolicyByClientRepository.Get(id: id_cliente, connectionString: connectionString, pageNumber: pageNumber);
    if (policies.Count() == 0) return Results.NotFound(new { message = "Nenhuma apólice encontrada para o cliente." });

    return Results.Ok(policies);
  }
}
