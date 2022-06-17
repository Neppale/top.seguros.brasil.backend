using Dapper;
using Microsoft.Data.SqlClient;

abstract class DeleteClienteService
{
  public static IResult Delete(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se o cliente existe.
    bool isExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente FROM Clientes WHERE id_cliente = @Id AND status = 'true'", new { Id = id });
    if (!isExistent) return Results.NotFound("Cliente não encontrado.");

    try
    {
      connectionString.Query("UPDATE Clientes SET status = 'false' WHERE id_cliente = @Id", new { Id = id });
      return Results.StatusCode(204);
    }
    catch (SystemException)
    {

      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}