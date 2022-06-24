static class GetAllApoliceService
{
  /** <summary> Esta função retorna as apólices no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Apolice>("SELECT * from Apolices WHERE status != 'Rejeitada'");

    return Results.Ok(data);
  }
}