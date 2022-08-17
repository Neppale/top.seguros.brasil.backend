public static class InsertVehicleService
{
  /** <summary> Esta função insere um Veiculo no banco de dados. </summary>**/
  public static async Task<IResult> Insert(Veiculo vehicle, SqlConnection connectionString)
  {
    // Verificando se alguma das propriedades do Veiculo é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(vehicle);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Por padrão, o status do veículo é true.
    vehicle.status = true;

    // Verificando se o ano é válido. Lembrando que o ano é composto de ano + combustível. Ex: "2019 Flex".
    bool isValidYear = VehicleYearValidator.Validate(vehicle.ano);
    if (!isValidYear) return Results.BadRequest("O ano do veículo não segue o formato correto. O ano é composto de ano + combustível. Ex: 2019 Flex");

    // Verificando se o RENAVAM é válido.
    bool RenavamIsValid = RenavamValidator.Validate(vehicle.renavam);
    if (!RenavamIsValid) return Results.BadRequest("O RENAVAM informado é inválido.");

    // Formatando modelo do veículo para passar na validação da API da FIPE.
    vehicle.modelo = VehicleModelFormatter.Format(vehicle.modelo);

    // Verificando se os dados do veículo são validados pela API da FIPE.
    bool vehicleIsValid = await VehicleFIPEValidator.Validate(vehicle.marca, vehicle.modelo, vehicle.ano);
    if (!vehicleIsValid) return Results.BadRequest("Este veículo não existe na tabela FIPE. Confira todos os campos e tente novamente.");

    // Verificando se placa ou RENAVAM já existem em outro veiculo no banco de dados.
    bool plateOrRenavamIsValid = VehicleAlreadyExistsValidator.Validate(vehicle: vehicle, connectionString: connectionString);
    if (!plateOrRenavamIsValid) return Results.BadRequest("A placa ou o RENAVAM informado já está sendo utilizado em outro veículo.");

    // Verificando se o cliente existe.
    var client = GetOneClientService.Get(id: vehicle.id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound("Cliente não encontrado.");

    var result = InsertVehicleRepository.Insert(veiculo: vehicle, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/veiculo/{result}", new { id_veiculo = result });
  }
}