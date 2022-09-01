static class DeleteClientService
{
  public static IResult Delete(int id, SqlConnection connectionString)
  {
    var client = GetClientByIdRepository.Get(id: id, connectionString: connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    var policies = GetPolicyByClientRepository.Get(id: id, connectionString: connectionString, pageNumber: 1, size: int.MaxValue);
    if (policies.Any(policy => policy.status == "Ativa")) return Results.BadRequest(new { message = "Não é possível desativar um cliente com apólices ativas." });

    var incidents = GetIncidentByClientRepository.Get(id: id, connectionString: connectionString, pageNumber: 1, size: int.MaxValue);
    if (incidents.Any(incident => incident.status == "Andamento" || incident.status == "Processando")) return Results.BadRequest(new { message = "Não é possível desativar um cliente com ocorrências ativas ou em processamento." });

    var result = DeleteClientRepository.Delete(id: id, connectionString: connectionString);
    if (result == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    var vehicles = GetVehiclesByClientRepository.Get(id: id, connectionString: connectionString, pageNumber: 1, size: int.MaxValue);
    foreach (var vehicle in vehicles) DeleteVehicleRepository.Delete(id: vehicle.id_veiculo, connectionString: connectionString);

    return Results.NoContent();
  }
}