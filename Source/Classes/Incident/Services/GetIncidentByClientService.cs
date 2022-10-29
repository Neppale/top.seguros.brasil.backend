static class GetIncidentByClientService
{
  public static async Task<IResult> Get(int id_cliente, SqlConnection connectionString, int? pageNumber, int? size)
  {
    if (pageNumber == null) pageNumber = 1;
    if (size == null) size = 5;

    var results = await GetIncidentByClientRepository.Get(id: id_cliente, connectionString: connectionString, pageNumber: pageNumber, size: size);

    return Results.Ok(results);
  }
}