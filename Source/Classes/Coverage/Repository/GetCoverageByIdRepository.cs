static class GetCoverageByIdRepository
{
  public static Cobertura Get(int id, SqlConnection connectionString)
  {
    var coverage = connectionString.QueryFirstOrDefault<Cobertura>("SELECT * FROM Coberturas WHERE id_cobertura = @Id AND status = 'true'", new { Id = id });
    return coverage;
  }

}