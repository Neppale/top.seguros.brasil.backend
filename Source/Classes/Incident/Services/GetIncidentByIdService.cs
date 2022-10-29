static class GetIncidentByIdService
{
  /** <summary> Esta função retorna uma ocorrência específica no banco de dados. </summary>**/
  public static async Task<IResult> Get(int id, SqlConnection connectionString)
  {
    var incident = await GetIncidentByIdRepository.Get(id, connectionString);
    if (incident == null) return Results.NotFound(new { message = "Ocorrência não encontrada." });

    return Results.Ok(incident);
  }
}