static class GetCoverageByIdRepository
{
  public static async Task<Cobertura> Get(int id, SqlConnection connectionString)
  {
    var coverage = await connectionString.QueryFirstOrDefaultAsync<Cobertura>("SELECT * FROM Coberturas WHERE id_cobertura = @Id AND status = 'true'", new { Id = id });
    return coverage;
  }

}