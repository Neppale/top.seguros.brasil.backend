using Dapper;
using Microsoft.Data.SqlClient;
abstract class GetOneClienteService
{
  /** <summary> Esta função retorna um cliente em específico no banco de daods. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.QueryFirstOrDefault<Cliente>("SELECT * FROM Clientes WHERE id_cliente = @Id", new { Id = id });
    if (data == null) return Results.NotFound("Cliente não encontrado.");

    return Results.Ok(data);
  }
}