public static class VehicleController
{
    public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString)
    {
        app.MapGet("/veiculo/", [Authorize] async (int? pageNumber, int? size, string? search) =>
        {
            return await GetAllVehicleService.Get(connectionString: connectionString, pageNumber: pageNumber, size: size, search: search);
        })
        .WithName("Selecionar todos os veículos");

        app.MapGet("/veiculo/{id:int}", [Authorize] async (int id) =>
        {
            return await GetVehicleByIdService.Get(id: id, connectionString: connectionString);
        })
        .WithName("Selecionar veículo específico");

        app.MapGet("/veiculo/cliente/{id:int}", [Authorize] async (int id, int? pageNumber, int? size) =>
        {
            return await GetVehiclesByClientService.Get(id_cliente: id, pageNumber: pageNumber, connectionString: connectionString, size: size);
        })
        .WithName("Selecionar veículo por cliente");

        app.MapPost("/veiculo/", [AllowAnonymous] async (Veiculo veiculo) =>
        {
            return await InsertVehicleService.Insert(vehicle: veiculo, connectionString: connectionString);
        })
        .WithName("Inserir veículo");

        app.MapPut("/veiculo/{id:int}", [Authorize] async (int id, Veiculo veiculo) =>
        {
            return await UpdateVehicleService.Update(id: id, vehicle: veiculo, connectionString: connectionString);
        })
        .WithName("Alterar veículo específico");

        app.MapDelete("/veiculo/{id:int}", [Authorize] async (int id) =>
        {
            return await DeleteVehicleService.Delete(id: id, connectionString: connectionString);
        })
        .WithName("Deletar veículo específico");

    }
}
