static class GetAllClientService
{
  /** <summary> Esta função retorna todos os clientes no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber)
  {
    if (pageNumber == null) pageNumber = 1;

    var clients = GetAllClientRepository.Get(connectionString: connectionString, pageNumber: pageNumber);

    return Results.Ok(clients);
  }
}