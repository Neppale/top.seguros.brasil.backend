using Dapper;
using Microsoft.Data.SqlClient;


public static class GetOneCoberturaService
{
  /** <summary> Esta função retorna uma cobertura específica no banco de dados. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault<Cobertura>("SELECT * from Coberturas WHERE id_cobertura = @Id", new { Id = id });

    if (data == null) return Results.NotFound("Cobertura não encontrada.");

    return Results.Ok(data);
  }
}
