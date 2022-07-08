public static class GetAllCoverageService
{
  /** <summary> Esta função retorna todas as coberturas no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString, int? pageNumber)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    var data = connectionString.Query("SELECT * from Coberturas WHERE status = 'true' ORDER BY id_cobertura OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });

    return Results.Ok(data);
  }
}
