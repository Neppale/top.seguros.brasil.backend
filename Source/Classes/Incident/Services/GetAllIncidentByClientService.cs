static class GetAllIncidentsByClientService
{
  public static IResult Get(int id_cliente, SqlConnection connectionString, int? pageNumber)
  {
    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    var results = GetAllIncidentByClientRepository.Get(id: id_cliente, connectionString: connectionString, pageNumber: pageNumber);

    return Results.Ok(results);
  }
}