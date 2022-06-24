public static class GetAllTerceirizadoService
{
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Terceirizado>("SELECT * from Terceirizados WHERE status = 'true'");

    return Results.Ok(data);
  }
}