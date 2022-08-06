public static class GetAllVehicleService
{
  /** <summary> Esta função retorna todos os veículos no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber)
  {
    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    var result = GetAllVehicleRepository.Get(connectionString: connectionString, pageNumber: pageNumber);

    // Removendo caracteres especiais da exibição do modelo dos veículos da lista.
    foreach (var item in result)
    {
      item.modelo = VehicleModelUnformatter.Unformat(item.modelo);
    }
    return Results.Ok(result);
  }
}