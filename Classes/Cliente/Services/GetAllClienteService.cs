using Dapper;
using Microsoft.Data.SqlClient;
abstract class GetAllClienteService
{
  /** <summary> Esta função retorna todos os clientes no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.QueryFirstOrDefault<Cliente>("SELECT * FROM Clientes");

    return Results.Ok(data);
  }
}