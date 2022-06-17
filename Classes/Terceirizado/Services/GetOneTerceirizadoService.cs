using Dapper;
using Microsoft.Data.SqlClient;
public abstract class GetOneTerceirizadoService
{
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault<Terceirizado>("SELECT * from Terceirizados WHERE id_terceirizado = @Id", new { Id = id });

    if (data == null) return Results.NotFound("Terceirizado não encontrado");
    return Results.Ok(data);
  }
}
