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
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    ocorrencia.id_terceirizado = originalTerceirizado;

    ocorrencia.status = ocorrencia.status.Substring(0, 1).ToUpper() + ocorrencia.status.Substring(1);

    if (!validStatuses.Contains(ocorrencia.status)) return Results.BadRequest("Status inválido. Status permitidos: " + string.Join(", ", validStatuses));

    ocorrencia.data = SqlDateConverter.Convert(ocorrencia.data);

    var incident = GetOneIncidentRepository.Get(id: id, connectionString: connectionString);
    if (incident == null) return Results.NotFound("Ocorrência não encontrada.");

    var client = GetOneClientRepository.Get(id: ocorrencia.id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound("Cliente não encontrado.");

    var vehicle = GetOneVehicleRepository.Get(id: ocorrencia.id_veiculo, connectionString: connectionString);
    if (vehicle == null) return Results.NotFound("Veículo não encontrado.");

    bool veiculoIsValid = ClientVehicleValidator.Validate(id_cliente: ocorrencia.id_cliente, id_veiculo: ocorrencia.id_veiculo, connectionString: connectionString);
    if (!veiculoIsValid) return Results.BadRequest("Veículo não pertence ao cliente.");

    if (ocorrencia.id_terceirizado != null)
    {
      var outsourced = GetOneOutsourcedRepository.Get(id: (int)ocorrencia.id_terceirizado, connectionString: connectionString);
      if (outsourced == null) return Results.NotFound("Terceirizado não encontrado.");
    }

    var storedDocument = GetIncidentDocumentRepository.Get(id: id, connectionString: connectionString);
    if (ocorrencia.status == "Concluida" && storedDocument.documento == null) return Results.BadRequest("Não é possível alterar o status de uma ocorrência para Concluída sem um documento.");

    var result = UpdateIncidentRepository.Update(incident: ocorrencia, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Ok("Ocorrência atualizada com sucesso.");
  }
}
