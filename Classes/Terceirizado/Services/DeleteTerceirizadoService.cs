using Dapper;
using Microsoft.Data.SqlClient;

public static class DeleteTerceirizadoService
{
  /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
  public static IResult Delete(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se terceirizado existe.
    bool isExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_terceirizado from Terceirizados WHERE id_terceirizado = @Id AND status = 'true'", new { Id = id });
    if (!isExistent) return Results.NotFound("Terceirizado não encontrado");

    try
    {
      connectionString.Query("UPDATE Terceirizados SET status = 'false' WHERE id_terceirizado = @Id", new { Id = id });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}
