public static class GetOneCoverageService
{
  /** <summary> Esta função retorna uma cobertura específica no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var coverage = GetOneCoverageRepository.Get(id: id, connectionString: connectionString);
    if (coverage == null) return Results.NotFound("Cobertura não encontrada.");

    return Results.Ok(coverage);
  }
}
