public static class GetAllVehicleService
{
  /** <summary> Esta função retorna todos os veículos no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    if (pageNumber == null) pageNumber = 1;
    if (size == null) size = 5;

    var vehicles = GetAllVehicleRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);

    return Results.Ok(vehicles);
  }
}