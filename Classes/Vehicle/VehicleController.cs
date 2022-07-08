public static class VehicleController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/veiculo/", [Authorize] (int? pageNumber) =>
    {
      return GetAllVehicleService.Get(dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todos os veículos");

    app.MapGet("/veiculo/{id:int}", [Authorize] (int id) =>
    {
      return GetOneVehicleService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar veículo específico");

    app.MapGet("/veiculo/cliente/{id:int}", [Authorize] (int id, int? pageNumber) =>
    {
      return GetVehiclesByClient.Get(id_cliente: id, pageNumber: pageNumber, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar veículo por cliente");

    app.MapPost("/veiculo/", [Authorize] (Veiculo veiculo) =>
    {
      return InsertVehicleService.Insert(veiculo: veiculo, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir veículo");

    app.MapPut("/veiculo/{id:int}", [Authorize] (int id, Veiculo veiculo) =>
    {
      return UpdateVehicleService.Update(id: id, veiculo: veiculo, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar veículo específico");

    app.MapDelete("/veiculo/{id:int}", [Authorize] (int id) =>
    {
      return DeleteVehicleService.Delete(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Deletar veículo específico");

  }
}
