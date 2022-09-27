static class GetAllCoverageRepository
{
  public static async Task<IEnumerable<Cobertura>> Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    var coverages = await connectionString.QueryAsync<Cobertura>("SELECT * from Coberturas WHERE status = 'true' ORDER BY id_cobertura DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
    return coverages;

  }
}