public static class DeleteVehicleService
{
  /** <summary> Esta função deleta um Veículo no banco de dados. </summary>**/
  public static IResult Delete(int id, SqlConnection connectionString)
  {
    var vehicle = GetOneVehicleRepository.Get(id: id, connectionString: connectionString);
    if (vehicle == null) return Results.NotFound("Veículo não encontrado.");

    var result = DeleteVehicleRepository.Delete(id: id, connectionString: connectionString);
    if (result == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.NoContent();
  }
}