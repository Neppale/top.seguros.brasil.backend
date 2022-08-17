static class GetOneIncidentService
{
  /** <summary> Esta função retorna uma ocorrência específica no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var incident = GetOneIncidentRepository.Get(id, connectionString);
    if (incident == null) return Results.NotFound("Ocorrência não encontrada.");

    return Results.Ok(incident);
  }
}