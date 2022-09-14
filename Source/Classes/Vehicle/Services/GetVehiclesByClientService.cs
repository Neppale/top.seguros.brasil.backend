static class GetVehiclesByClientService
{
  /** <summary>Esta função retorna todos os veículos do cliente.</summary> **/
  public static IResult Get(int id_cliente, SqlConnection connectionString, int? pageNumber, int? size)
  {

    var client = GetClientByIdRepository.Get(id: id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    if (pageNumber == null) pageNumber = 1;
    if (size == null) size = 5;

    var vehicles = GetVehiclesByClientRepository.Get(id: id_cliente, connectionString: connectionString, pageNumber: pageNumber, size: size);

    return Results.Ok(vehicles);
  }
}