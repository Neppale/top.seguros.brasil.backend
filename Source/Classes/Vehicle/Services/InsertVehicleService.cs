public static class InsertVehicleService
{
    /** <summary> Esta função insere um Veiculo no banco de dados. </summary>**/
    public static async Task<IResult> Insert(Veiculo vehicle, SqlConnection connectionString)
    {
        bool hasValidProperties = NullPropertyValidator.Validate(vehicle);
        if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

        vehicle.status = true;

        bool isValidYear = VehicleYearValidator.Validate(vehicle.ano);
        if (!isValidYear) return Results.BadRequest(new { message = "O ano do veículo não segue o formato correto. O ano é composto de ano + combustível. Ex: 2019 Flex" });

        bool RenavamIsValid = RenavamValidator.Validate(vehicle.renavam);
        if (!RenavamIsValid) return Results.BadRequest(new { message = "O RENAVAM informado é inválido." });

        bool vehicleIsValid = await VehicleFIPEValidator.Validate(vehicle.marca, vehicle.modelo, vehicle.ano);
        if (!vehicleIsValid) return Results.BadRequest(new { message = "Este veículo não existe na tabela FIPE. Confira todos os campos e tente novamente." });

        bool plateOrRenavamIsValid = await VehicleAlreadyExistsValidator.Validate(vehicle: vehicle, connectionString: connectionString);
        if (!plateOrRenavamIsValid) return Results.BadRequest(new { message = "A placa ou o RENAVAM informado já está sendo utilizado em outro veículo." });

        var client = await GetClientByIdRepository.Get(id: vehicle.id_cliente, connectionString: connectionString);
        if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

        var createdVehicle = await InsertVehicleRepository.Insert(vehicle: vehicle, connectionString: connectionString);
        if (createdVehicle == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

        return Results.Created($"/veiculo/{createdVehicle.id_veiculo}", new { message = "Veículo criado com sucesso.", vehicle = createdVehicle });
    }
}