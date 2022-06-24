public static class GetAllCoberturaService
{
  /** <summary> Esta função retorna todas as coberturas no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Cobertura>("SELECT * from Coberturas WHERE status = 'true'");

    return Results.Ok(data);
  }
}
