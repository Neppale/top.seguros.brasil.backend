public static class DeleteVehicleService
{
    /** <summary> Esta função deleta um Veículo no banco de dados. </summary>**/
    public static async Task<IResult> Delete(int id, SqlConnection connectionString)
    {
        var vehicle = await GetVehicleByIdRepository.Get(id: id, connectionString: connectionString);
        if (vehicle == null) return Results.NotFound(new { message = "Veículo não encontrado." });

        var policies = await GetPolicyByClientRepository.Get(id: vehicle.id_cliente, connectionString: connectionString, pageNumber: 1, size: int.MaxValue);
        if (policies.Any(policy => (policy.status == "Ativa" || policy.status == "Em Analise") && policy.id_veiculo == vehicle.id_veiculo)) return Results.BadRequest(new { message = "Não é possível desativar um veículo com apólices ativas ou em análise." });

        var incidents = await GetIncidentByClientRepository.Get(id: vehicle.id_cliente, connectionString: connectionString, pageNumber: 1, size: int.MaxValue);
        if (incidents.Any(incident => (incident.status == "Andamento" || incident.status == "Processando") && incident.id_veiculo == vehicle.id_veiculo)) return Results.BadRequest(new { message = "Não é possível desativar um veículo com ocorrências ativas ou em processamento." });

        var result = await DeleteVehicleRepository.Delete(id: id, connectionString: connectionString);
        if (result == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

        return Results.NoContent();
    }
}