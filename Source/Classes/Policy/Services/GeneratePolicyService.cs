static class GeneratePolicyService
{

  public static async Task<IResult> Generate(int id_cliente, int id_veiculo, int id_cobertura, SqlConnection connectionString)
  {
    var client = GetOneClientRepository.Get(id_cliente, connectionString);
    if (client == null) return Results.BadRequest("Cliente não encontrado.");

    var vehicle = GetOneVehicleRepository.Get(id_veiculo, connectionString);
    if (vehicle == null) return Results.BadRequest("Veiculo não encontrado.");

    decimal vehicleValue = await VehiclePriceFinder.Find(vehicle.marca, vehicle.modelo, vehicle.ano);

    var coverage = GetOneCoverageRepository.Get(id_cobertura, connectionString);
    if (coverage == null) return Results.BadRequest("Cobertura não encontrada.");

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