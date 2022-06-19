using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
abstract class DeleteVeiculoService
{
  /** <summary> Esta função deleta um Veículo no banco de dados. </summary>**/
  public static IResult Delete(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se veículo existe.
    bool isExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo from Veiculos WHERE id_Veiculo = @Id", new { Id = id });
    if (!isExistent) return Results.NotFound("Veículo não encontrado.");

    try
    {
      connectionString.Query("DELETE FROM Veiculos WHERE id_Veiculo = @Id", new { Id = id });

      return Results.NoContent();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}