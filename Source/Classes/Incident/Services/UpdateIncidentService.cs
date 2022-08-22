static class UpdateIncidentService
{

  private static string[] validStatuses = { "Andamento", "Processando", "Concluida", "Rejeitada" };
  public static IResult Update(int id, Ocorrencia ocorrencia, SqlConnection connectionString)
  {
    ocorrencia.documento = "-";

    int? originalTerceirizado = ocorrencia.id_terceirizado;
    ocorrencia.id_terceirizado = 0;

    ocorrencia.tipoDocumento = "-";

    bool hasValidProperties = NullPropertyValidator.Validate(ocorrencia);
    if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

    ocorrencia.id_terceirizado = originalTerceirizado;

    ocorrencia.status = ocorrencia.status.Substring(0, 1).ToUpper() + ocorrencia.status.Substring(1);

    if (!validStatuses.Contains(ocorrencia.status)) return Results.BadRequest("Status inválido. Status permitidos: " + string.Join(", ", validStatuses));

    ocorrencia.data = SqlDateConverter.Convert(ocorrencia.data);

    var incident = GetIncidentByIdRepository.Get(id: id, connectionString: connectionString);
    if (incident == null) return Results.NotFound("Ocorrência não encontrada.");

    var client = GetClientByIdRepository.Get(id: ocorrencia.id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    var vehicle = GetOneVehicleRepository.Get(id: ocorrencia.id_veiculo, connectionString: connectionString);
    if (vehicle == null) return Results.NotFound(new { message = "Veículo não encontrado." });

    bool veiculoIsValid = ClientVehicleValidator.Validate(id_cliente: ocorrencia.id_cliente, id_veiculo: ocorrencia.id_veiculo, connectionString: connectionString);
    if (!veiculoIsValid) return Results.BadRequest(new { message = "Veículo não pertence ao cliente." });

    if (ocorrencia.id_terceirizado != null)
    {
      var outsourced = GetOneOutsourcedRepository.Get(id: (int)ocorrencia.id_terceirizado, connectionString: connectionString);
      if (outsourced == null) return Results.NotFound(new { message = "Terceirizado não encontrado." });
    }

    var storedDocument = GetIncidentDocumentRepository.Get(id: id, connectionString: connectionString);
    if (ocorrencia.status == "Concluida" && storedDocument.documento == null) return Results.BadRequest(new { message = "Não é possível alterar o status de uma ocorrência para Concluída sem um documento." });

    var updatedIncident = UpdateIncidentRepository.Update(id: id, incident: ocorrencia, connectionString: connectionString);
    if (updatedIncident == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.Ok(new { message = "Ocorrência atualizada com sucesso.", incident = updatedIncident });
  }
}
