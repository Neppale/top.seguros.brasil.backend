static class GeneratePolicyService
{

  public static async Task<IResult> Generate(int id_cliente, int id_veiculo, int id_cobertura, SqlConnection connectionString)
  {
    // Verificando se cliente existe e está ativo.
    var client = GetOneClientRepository.Get(id_cliente, connectionString);
    if (client == null) return Results.BadRequest("Cliente não encontrado.");

    // Verificando se veiculo existe e está ativo.
    var vehicle = GetOneVehicleRepository.Get(id_veiculo, connectionString);
    if (vehicle == null) return Results.BadRequest("Veiculo não encontrado.");

    // Recuperando valor do veículo na tabela FIPE.
    decimal vehicleValue = await VehiclePriceFinder.Find(vehicle.marca, vehicle.modelo, vehicle.ano);

    // Verificando se cobertura existe e está ativa.
    var coverage = GetOneCoverageRepository.Get(id_cobertura, connectionString);
    if (coverage == null) return Results.BadRequest("Cobertura não encontrada.");

    // Verificando se o veículo realmente pertence ao cliente.
    bool vehicleBelongsToClient = ClientVehicleValidator.Validate(id_cliente, id_veiculo, connectionString);
    if (!vehicleBelongsToClient) return Results.BadRequest("Veículo não pertence ao cliente.");

    try
    {
      Apolice generatedApolice = new(
        data_inicio: SqlDateConverter.Convert(DateTime.Now.AddDays(5).ToString("dd/MM/yyyy")),
        data_fim: SqlDateConverter.Convert(DateTime.Now.AddDays(5).AddYears(1).ToString("dd/MM/yyyy")),
        premio: PremiumCalculator.Calculate(vehicleValue: vehicleValue, id_cobertura: id_cobertura, connectionString: connectionString),
        indenizacao: IndemnisationCalculator.Calculate(id_cobertura: id_cobertura, vehicleValue: vehicleValue, connectionString: connectionString),
        documento: "-",
        id_cliente: id_cliente,
        id_cobertura: id_cobertura,
        id_veiculo: id_veiculo,
        id_usuario: UsuarioSelector.Select(connectionString),
        status: "Em Análise"
      );

      return Results.Ok(new { message = "Apólice gerada com sucesso.", warning = "Lembre-se de que a apólice não foi salva no banco de dados, apenas gerada, e por isso seu id é 0.", apolice = generatedApolice });
    }
    catch (SystemException)
    {
      return Results.BadRequest("Erro ao gerar apólice. Tente novamente mais tarde.");
    }

  }
}