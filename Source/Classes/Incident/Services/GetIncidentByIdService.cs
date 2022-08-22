static class GetIncidentByIdService
{
  /** <summary> Esta função retorna uma ocorrência específica no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var incident = GetIncidentByIdRepository.Get(id, connectionString);
    if (incident == null) return Results.NotFound(new { message = "Ocorrência não encontrada." });

    return Results.Ok(incident);
  }
}