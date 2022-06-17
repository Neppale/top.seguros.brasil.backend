using Dapper;
using Microsoft.Data.SqlClient;
abstract class GetAllVeiculoService
{
  /** <summary> Esta função retorna todos os veículos no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Veiculo>("SELECT * from Veiculos");

    return Results.Ok(data);
  }
}