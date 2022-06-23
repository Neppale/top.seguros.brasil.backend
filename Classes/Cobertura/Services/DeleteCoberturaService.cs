using Dapper;
using Microsoft.Data.SqlClient;


public static class DeleteCoberturaService
{
  /** <summary> Esta função desativa uma cobertura no banco de dados. </summary>**/
  public static IResult Delete(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se a cobertura existe.
    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_cobertura from Coberturas WHERE id_cobertura = @Id", new { Id = id });
    if (!Exists) return Results.NotFound("Cobertura não encontrada.");

    try
    {
      connectionString.Query("UPDATE Coberturas SET status = 'false' WHERE id_cobertura = @Id", new { Id = id });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}