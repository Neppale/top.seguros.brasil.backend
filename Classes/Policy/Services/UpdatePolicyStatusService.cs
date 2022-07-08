static class UpdatePolicyStatusService
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

    // Se status for "Analise", alterar para "Em Análise".
    if (status == "Analise") status = "Em Análise";

    // Verificando se apólice existe.
    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_apolice from Apolices WHERE id_apolice = @Id", new { Id = id });
    if (!Exists) return Results.NotFound("Apólice não encontrada.");

    // Verificando se o status é igual ao atual.
    string currentStatus = connectionString.QueryFirstOrDefault<string>("SELECT status from Apolices WHERE id_apolice = @Id", new { Id = id });
    if (currentStatus == status) return Results.Conflict("O novo status da apólice não pode ser igual ao atual.");
    if (currentStatus == "Rejeitada" || currentStatus == "Inativa") return Results.Conflict($"O status desta apólice não pode ser alterado. Status atual: {currentStatus}");


    try
    {
      connectionString.Query<Apolice>("UPDATE Apolices SET status = @Status WHERE id_apolice = @Id", new { Id = id, Status = status });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");
    }
  }
}