using Dapper;
using Microsoft.Data.SqlClient;

public static class GetAllVeiculoService
{
  /** <summary> Esta função retorna todos os veículos no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Veiculo>("SELECT * from Veiculos WHERE status = 'true'");

    return Results.Ok(data);
  }
}