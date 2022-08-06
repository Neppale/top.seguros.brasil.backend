public static class UpdateVehicleService
{
  /** <summary> Esta função altera um Veículo no banco de dados. </summary>**/
  public static async Task<IResult> Update(int id, Veiculo vehicle, SqlConnection connectionString)
  {
    // Verificando se veículo existe.
    var vehicleExists = GetOneVehicleRepository.Get(id: id, connectionString: connectionString);
    if (vehicleExists == null) return Results.NotFound("Veículo não encontrado.");

    // Verificando se alguma das propriedades do veiculo é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(vehicle);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificando se o RENAVAM é válido.
    bool renavamIsValid = RenavamValidator.Validate(vehicle.renavam);
    if (!renavamIsValid) return Results.BadRequest("O RENAVAM informado é inválido.");

    // Formatando modelo do veículo para passar na validação da API da FIPE.
    vehicle.modelo = VehicleModelFormatter.Format(vehicle.modelo);

    // Verificando se os dados do veículo existem na API da FIPE.
    bool veiculoIsValid = await VehicleFIPEValidator.Validate(vehicle.marca, vehicle.modelo, vehicle.ano);
    if (!veiculoIsValid) return Results.BadRequest("Este veículo não existe na tabela FIPE. Confira todos os campos e tente novamente.");

    // Verificando se placa ou RENAVAM já existe em outro veiculo no banco de dados.
    bool plateOrRenavamAlreadyExists = VehicleAlreadyExistsValidator.Validate(id: id, vehicle: vehicle, connectionString: connectionString);
    if (!plateOrRenavamAlreadyExists) return Results.BadRequest("A placa ou o RENAVAM informado já está sendo utilizado em outro veículo.");

    var result = UpdateVehicleRepository.Update(id: id, veiculo: vehicle, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Ok("Veículo alterado com sucesso.");
  }
}