static class DeleteClientService
{
  public static async Task<IResult> Delete(int id, SqlConnection connectionString)
  {
    var client = await GetClientByIdRepository.Get(id: id, connectionString: connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    var policies = await GetPolicyByClientRepository.Get(id: id, connectionString: connectionString, pageNumber: 1, size: int.MaxValue);
    if (policies.Any(policy => policy.status == "Ativa")) return Results.BadRequest(new { message = "Não é possível desativar um cliente com apólices ativas." });

    var incidents = await GetIncidentByClientRepository.Get(id: id, connectionString: connectionString, pageNumber: 1, size: int.MaxValue);
    if (incidents.Any(incident => incident.status == "Andamento" || incident.status == "Processando")) return Results.BadRequest(new { message = "Não é possível desativar um cliente com ocorrências ativas ou em processamento." });

    var result = await DeleteClientRepository.Delete(id: id, connectionString: connectionString);
    if (result == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    var vehicles = await GetVehiclesByClientRepository.Get(id: id, connectionString: connectionString, pageNumber: 1, size: int.MaxValue);
    foreach (var vehicle in vehicles.vehicles)
    {
      await DeleteVehicleRepository.Delete(id: vehicle.id_veiculo, connectionString: connectionString);
    }

    return Results.NoContent();
  }
}