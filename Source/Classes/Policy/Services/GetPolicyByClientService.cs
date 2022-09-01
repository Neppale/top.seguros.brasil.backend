static class GetPolicyByClientService
{
  public static IResult Get(int id_cliente, int? pageNumber, SqlConnection connectionString, int? size)
  {

    var client = GetClientByIdRepository.Get(id: id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    if (pageNumber == null) pageNumber = 1;
    if (size == null) size = 5;

    var policies = GetPolicyByClientRepository.Get(id: id_cliente, connectionString: connectionString, pageNumber: pageNumber, size: size);
    return Results.Ok(policies);
  }
}
