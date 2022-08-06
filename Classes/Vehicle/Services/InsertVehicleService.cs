public static class InsertVehicleService
{
  /** <summary> Esta função insere um Veiculo no banco de dados. </summary>**/
  public static async Task<IResult> Insert(Veiculo veiculo, SqlConnection connectionString)
  {
    // Verificando se alguma das propriedades do Veiculo é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(veiculo);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Por padrão, o status do veículo é true.
    veiculo.status = true;

    // Verificando se o ano é válido. Lembrando que o ano é composto de ano + combustível. Ex: "2019 Flex".
    bool isValidYear = VehicleYearValidator.Validate(veiculo.ano);
    if (!isValidYear) return Results.BadRequest("O ano do veículo não segue o formato correto.");

    // Verificando se o RENAVAM é válido.
    bool RenavamIsValid = RenavamValidator.Validate(veiculo.renavam);
    if (!RenavamIsValid) return Results.BadRequest("O RENAVAM informado é inválido.");

    // Formatando modelo do veículo para passar na validação da API da FIPE.
    veiculo.modelo = VehicleModelFormatter.Format(veiculo.modelo);

    // Verificando se os dados do veículo são validados pela API da FIPE.
    bool vehicleIsValid = await VehicleFIPEValidator.Validate(veiculo.marca, veiculo.modelo, veiculo.ano);
    if (!vehicleIsValid) return Results.BadRequest("Este veículo não existe na tabela FIPE. Confira todos os campos e tente novamente.");

    // Verificando se o RENAVAM já existe em outro veiculo no banco de dados.
    bool renavamAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT renavam FROM Veiculos WHERE renavam = @Renavam) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Renavam = veiculo.renavam });
    if (renavamAlreadyExists) return Results.Conflict("O RENAVAM informado já está sendo utilizado por outro veiculo.");

    // Verificando se a placa já existe em outro veiculo no banco de dados.
    bool placaAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT placa FROM Veiculos WHERE placa = @Placa) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Placa = veiculo.placa });
    if (placaAlreadyExists) return Results.Conflict("A placa informada já está sendo utilizada por outro veiculo.");

    // Verificando se o cliente existe.
    var client = GetOneClientService.Get(id: veiculo.id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound("Cliente não encontrado.");

    var result = InsertVehicleRepository.Insert(veiculo: veiculo, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/veiculo/{result}", new { id_veiculo = result });
  }
}