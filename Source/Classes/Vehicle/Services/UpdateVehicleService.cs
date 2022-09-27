public static class UpdateVehicleService
{
  /** <summary> Esta função altera um Veículo no banco de dados. </summary>**/
  public static async Task<IResult> Update(int id, Veiculo vehicle, SqlConnection connectionString)
  {
    var vehicleExists = await GetVehicleByIdRepository.Get(id: id, connectionString: connectionString);
    if (vehicleExists == null) return Results.NotFound(new { message = "Veículo não encontrado." });

    bool hasValidProperties = NullPropertyValidator.Validate(vehicle);
    if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

    bool renavamIsValid = RenavamValidator.Validate(vehicle.renavam);
    if (!renavamIsValid) return Results.BadRequest(new { message = "O RENAVAM informado é inválido." });

    bool veiculoIsValid = await VehicleFIPEValidator.Validate(vehicle.marca, vehicle.modelo, vehicle.ano);
    if (!veiculoIsValid) return Results.BadRequest(new { message = "Este veículo não existe na tabela FIPE. Confira todos os campos e tente novamente." });

    bool plateOrRenavamAlreadyExists = await VehicleAlreadyExistsValidator.Validate(id: id, vehicle: vehicle, connectionString: connectionString);
    if (!plateOrRenavamAlreadyExists) return Results.BadRequest(new { message = "A placa ou o RENAVAM informado já está sendo utilizado em outro veículo." });

    var updatedVehicle = await UpdateVehicleRepository.Update(id: id, veiculo: vehicle, connectionString: connectionString);
    if (updatedVehicle == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.Ok(new { message = "Veículo alterado com sucesso.", vehicle = updatedVehicle });
  }
}