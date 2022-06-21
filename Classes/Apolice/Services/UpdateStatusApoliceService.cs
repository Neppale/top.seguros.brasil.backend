using Dapper;
using Microsoft.Data.SqlClient;
static class UpdateStatusApoliceService
{
  private static string[] apoliceStatuses = { "Ativa", "Inativa", "Em Análise", "Rejeitada" };

  /** <summary> Esta função altera o status de uma apólice no banco de dados. </summary>**/
  public static IResult UpdateStatus(int id, ApoliceStatus status, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verifica se o status passado é válido.
    if (!apoliceStatuses.Contains(status.status))
    {
      return Results.BadRequest("Status inválido. Status permitidos: " + string.Join(", ", apoliceStatuses));
    }

    // Verificando se apólice existe.
    bool isExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_apolice from Apolices WHERE id_apolice = @Id", new { Id = id });
    if (!isExistent) return Results.NotFound("Apólice não encontrada.");


    try
    {
      connectionString.Query<Apolice>("UPDATE Apolices SET status = @Status' WHERE id_apolice = @Id", new { Id = id, Status = status });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}