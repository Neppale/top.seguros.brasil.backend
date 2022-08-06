static class UpdatePolicyStatusService
{
  private static string[] validStatuses = { "Ativa", "Inativa", "Analise", "Rejeitada" };

  /** <summary> Esta função altera o status de uma apólice no banco de dados. </summary>**/
  public static IResult Update(int id, string status, SqlConnection connectionString)
  {
    // Letra inicial maiúscula para o status.
    status = status.Substring(0, 1).ToUpper() + status.Substring(1);

    // Verifica se o status passado é válido.
    if (!validStatuses.Contains(status)) return Results.BadRequest("Status inválido. Status permitidos: " + string.Join(", ", validStatuses));

    // Se status for "Analise", alterar para "Em Análise".
    if (status == "Analise") status = "Em Análise";

    // Verificando se apólice existe.
    var policy = GetOnePolicyRepository.Get(id: id, connectionString: connectionString);
    if (policy == null) return Results.NotFound("Apólice não encontrada.");

    // Verificando se o status é igual ao atual.
    if (policy.status == status) return Results.Conflict("O novo status da apólice não pode ser igual ao atual.");
    if (policy.status == "Rejeitada" || policy.status == "Inativa") return Results.Conflict($"O status desta apólice não pode ser alterado. Status atual: {policy}");

    var result = UpdatePolicyStatusRepository.Update(id: id, status: status, connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Ok();
  }
}