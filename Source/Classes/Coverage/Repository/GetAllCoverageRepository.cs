static class GetAllCoverageRepository
{
  public static IEnumerable<Cobertura> Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    var coverages = connectionString.Query<Cobertura>("SELECT * from Coberturas WHERE status = 'true' ORDER BY id_cobertura OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
    return coverages;

  }
}