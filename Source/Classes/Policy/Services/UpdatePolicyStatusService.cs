static class UpdatePolicyStatusService
{
  private static string[] validStatuses = { "Ativa", "Inativa", "Analise", "Rejeitada" };

  /** <summary> Esta função altera o status de uma apólice no banco de dados. </summary>**/
  public static async Task<IResult> Update(int id, string status, SqlConnection connectionString)
  {
    status = status.Substring(0, 1).ToUpper() + status.Substring(1);

    if (!validStatuses.Contains(status)) return Results.BadRequest(new { message = "Status inválido. Status permitidos: " + string.Join(", ", validStatuses) });

    if (status == "Analise") status = "Em Analise";

    var policy = await GetPolicyByIdRepository.Get(id: id, connectionString: connectionString);
    if (policy == null) return Results.NotFound("Apólice não encontrada.");

    if (policy.status == status) return Results.Conflict(new { message = "O novo status da apólice não pode ser igual ao atual." });
    if (policy.status == "Rejeitada" || policy.status == "Inativa") return Results.Conflict(new { message = $"O status desta apólice não pode ser alterado. Status atual: {policy.status}" });

    var result = await UpdatePolicyStatusRepository.Update(id: id, status: status, connectionString);
    if (result == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.Ok();
  }
}