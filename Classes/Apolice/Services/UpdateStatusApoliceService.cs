using Dapper;
using Microsoft.Data.SqlClient;
static class UpdateStatusApoliceService
{
  private static string[] validStatuses = { "Ativa", "Inativa", "Analise", "Rejeitada" };

  /** <summary> Esta função altera o status de uma apólice no banco de dados. </summary>**/
  public static IResult UpdateStatus(int id, string status, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Letra inicial maiúscula para o status.
    status = status.Substring(0, 1).ToUpper() + status.Substring(1);

    // Verifica se o status passado é válido.
    if (!validStatuses.Contains(status)) return Results.BadRequest("Status inválido. Status permitidos: " + string.Join(", ", validStatuses));

    // Verificando se apólice existe.
    bool isExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_apolice from Apolices WHERE id_apolice = @Id", new { Id = id });
    if (!isExistent) return Results.NotFound("Apólice não encontrada.");

    // Verificando se o status é igual ao atual.
    string currentStatus = connectionString.QueryFirstOrDefault<string>("SELECT status from Apolices WHERE id_apolice = @Id", new { Id = id });
    if (currentStatus == status) return Results.Conflict("Status novo da apólice não pode ser igual ao atual.");

    try
    {
      connectionString.Query<Apolice>("UPDATE Apolices SET status = @Status WHERE id_apolice = @Id", new { Id = id, Status = status });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}