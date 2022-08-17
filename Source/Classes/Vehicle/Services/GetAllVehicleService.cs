public static class GetAllVehicleService
{
  /** <summary> Esta função retorna todos os veículos no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber)
  {
    if (pageNumber == null) pageNumber = 1;

    var result = GetAllVehicleRepository.Get(connectionString: connectionString, pageNumber: pageNumber);

    foreach (var item in result)
    {
      item.modelo = VehicleModelUnformatter.Unformat(item.modelo);
    }
    return Results.Ok(result);
  }
}