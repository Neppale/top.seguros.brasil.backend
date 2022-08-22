public static class GetCoverageByIdService
{
  /** <summary> Esta função retorna uma cobertura específica no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var coverage = GetCoverageByIdRepository.Get(id: id, connectionString: connectionString);
    if (coverage == null) return Results.NotFound(new { message = "Cobertura não encontrada." });

    return Results.Ok(coverage);
  }
}
