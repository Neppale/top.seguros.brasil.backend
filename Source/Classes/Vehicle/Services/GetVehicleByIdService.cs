public static class GetVehicleByIdService
{
  /** <summary> Esta função retorna um veículo específico no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var vehicle = GetVehicleByIdRepository.Get(id: id, connectionString: connectionString);
    if (vehicle == null) return Results.NotFound(new { message = "Veículo não encontrado." });

    return Results.Ok(vehicle);
  }
}