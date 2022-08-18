static class GetVehiclesByClient
{
  /** <summary>Esta função retorna todos os veículos do cliente.</summary> **/
  public static IResult Get(int id_cliente, int? pageNumber, SqlConnection connectionString)
  {
    if (pageNumber == null) pageNumber = 1;

    var client = GetOneClientRepository.Get(id: id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    var vehicles = GetVehiclesByClientRepository.Get(id: id_cliente, connectionString: connectionString, pageNumber: pageNumber);

    foreach (var vehicle in vehicles) vehicle.modelo = VehicleModelUnformatter.Unformat(vehicle.modelo);

    return Results.Ok(vehicles);
  }
}