static class InsertIncidentService
{
  /** <summary> Esta função insere uma ocorrência no banco de dados. </summary>**/
  public static async Task<IResult> Insert(Ocorrencia ocorrencia, SqlConnection connectionString)
  {
    string? originalDocumento = ocorrencia.documento;
    if (ocorrencia.documento == null || ocorrencia.documento == "") ocorrencia.documento = "-";

    string? originalTipoDocumento = ocorrencia.tipoDocumento;
    if (ocorrencia.tipoDocumento == null || ocorrencia.tipoDocumento == "") ocorrencia.tipoDocumento = "-";

    ocorrencia.status = "Andamento";

    bool hasValidProperties = NullPropertyValidator.Validate(ocorrencia);
    if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

    bool dateIsValid = IncidentDateValidator.Validate(ocorrencia.data);
    if (!dateIsValid) return Results.BadRequest(new { message = "A data da ocorrência não pode ser maior que a data atual." });

    if (ocorrencia.id_terceirizado == 0) ocorrencia.id_terceirizado = null;

    if (ocorrencia.id_terceirizado != null)
    {
      var outsourced = await GetOutsourcedByIdRepository.Get(id: (int)ocorrencia.id_terceirizado, connectionString: connectionString);
      if (outsourced == null) return Results.NotFound(new { message = "Terceirizado não encontrado." });
    }

    ocorrencia.tipoDocumento = originalTipoDocumento;
    ocorrencia.documento = originalDocumento;
    ocorrencia.status = "Andamento";

    var client = await GetClientByIdRepository.Get(id: ocorrencia.id_cliente, connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    var vehicle = await GetVehicleByIdRepository.Get(id: ocorrencia.id_veiculo, connectionString);
    if (vehicle == null) return Results.NotFound(new { message = "Veículo não encontrado." });

    bool vehicleIsValid = await ClientVehicleValidator.Validate(id_cliente: ocorrencia.id_cliente, id_veiculo: ocorrencia.id_veiculo, connectionString: connectionString);
    if (!vehicleIsValid) return Results.BadRequest(new { message = "Veículo não pertence ao cliente." });

    var createdIncident = await InsertIncidentRepository.Insert(incident: ocorrencia, connectionString: connectionString);
    if (createdIncident == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.Created($"/ocorrencia/{createdIncident}", new { message = "Ocorrência criada com sucesso.", incident = createdIncident });
  }
}