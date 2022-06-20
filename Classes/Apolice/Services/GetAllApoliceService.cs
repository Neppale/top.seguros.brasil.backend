using Dapper;
using Microsoft.Data.SqlClient;
static class GetAllApoliceService
{
  /** <summary> Esta função retorna as apólices no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.QueryFirstOrDefault<Apolice>("SELECT * from Apolices");

    return Results.Ok(data);
  }
}