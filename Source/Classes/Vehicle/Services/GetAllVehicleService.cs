public static class GetAllVehicleService
{
  /** <summary> Esta função retorna todos os veículos no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber)
  {
    if (pageNumber == null) pageNumber = 1;

    var vehicles = GetAllVehicleRepository.Get(connectionString: connectionString, pageNumber: pageNumber);

    foreach (var vehicle in vehicles) vehicle.modelo = VehicleModelUnformatter.Unformat(vehicle.modelo);
    return Results.Ok(vehicles);
  }
}