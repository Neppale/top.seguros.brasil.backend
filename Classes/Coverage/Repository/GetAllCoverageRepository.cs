static class GetAllCoverageRepository
{
  public static IEnumerable<Cobertura> Get(SqlConnection connectionString, int? pageNumber)
  {
    var coverages = connectionString.Query<Cobertura>("SELECT * from Coberturas WHERE status = 'true' ORDER BY id_cobertura OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });
    return coverages;

  }
}