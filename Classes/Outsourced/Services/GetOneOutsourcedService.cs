public static class GetOneOutsourcedService
{
  /** <summary> Esta função retorna um terceirizado específico no banco de dados. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.QueryFirstOrDefault("SELECT * FROM Terceirizados WHERE id_terceirizado = @Id", new { Id = id });
    if (data == null) return Results.NotFound("Terceirizado não encontrado");

    return Results.Ok(data);
  }
}
