static class GeneratePolicyService
{

    public static async Task<IResult> Generate(int id_cliente, int id_veiculo, int id_cobertura, SqlConnection connectionString)
    {
        var client = await GetClientByIdRepository.Get(id_cliente, connectionString);
        if (client == null) return Results.BadRequest(new { message = "Cliente não encontrado." });

        var vehicle = await GetVehicleByIdRepository.Get(id_veiculo, connectionString);
        if (vehicle == null) return Results.BadRequest(new { message = "Veículo não encontrado." });

        decimal vehicleValue = await VehiclePriceFinder.Find(vehicle.marca, vehicle.modelo, vehicle.ano);

        var coverage = GetCoverageByIdRepository.Get(id_cobertura, connectionString);
        if (coverage == null) return Results.BadRequest(new { message = "Cobertura não encontrada." });

        bool vehicleBelongsToClient = await ClientVehicleValidator.Validate(id_cliente, id_veiculo, connectionString);
        if (!vehicleBelongsToClient) return Results.BadRequest(new { message = "Veículo não pertence ao cliente." });

        var user = await UserSelector.Select(connectionString);
        if (user == 0) return Results.BadRequest(new { message = "Nenhum corretor disponível." });

        try
        {
            GetPolicyDto generatedApolice = new(
            data_inicio: DateTime.Now.AddDays(5).ToString("yyyy-MM-dd"),
            data_fim: DateTime.Now.AddDays(5).AddYears(1).ToString("yyyy-MM-dd"),
            premio: await PremiumCalculator.Calculate(vehicleValue: vehicleValue, id_cobertura: id_cobertura, connectionString: connectionString),
            indenizacao: await IndemnisationCalculator.Calculate(id_cobertura: id_cobertura, vehicleValue: vehicleValue, connectionString: connectionString),
            id_cliente: id_cliente,
            id_cobertura: id_cobertura,
            id_veiculo: id_veiculo,
            id_usuario: user,
            status: "Em Analise"
          );

            var enrichedPolicy = await PolicyEnrichment.Enrich(generatedApolice, connectionString);

            return Results.Ok(new { message = "Modelo de apólice gerado com sucesso.", policy = generatedApolice, enrichedPolicy = enrichedPolicy });
        }
        catch (SystemException)
        {
            return Results.BadRequest(new { message = "Erro ao gerar apólice. Tente novamente mais tarde." });
        }
    }
}