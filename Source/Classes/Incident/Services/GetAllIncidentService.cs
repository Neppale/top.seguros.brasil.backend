static class GetAllIncidentService
{
  /** <summary> Esta função retorna todas as ocorrências no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber)
  {
    if (pageNumber == null) pageNumber = 1;

    var results = GetAllIncidentRepository.Get(connectionString: connectionString, pageNumber: pageNumber);

    return Results.Ok(results);
  }
}