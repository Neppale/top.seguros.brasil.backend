static class GetAllClientService
{
  /** <summary> Esta função retorna todos os clientes no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    if (pageNumber == null) pageNumber = 1;
    if (size == null) size = 5;

    var clients = GetAllClientRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);

    return Results.Ok(clients);
  }
}