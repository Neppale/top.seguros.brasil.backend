static class UpdateIncidentService
{

  private static string[] validStatuses = { "Andamento", "Processando", "Concluida", "Rejeitada" };
  public static IResult Update(int id, Ocorrencia ocorrencia, SqlConnection connectionString)
  {
    // Fazendo documento pular a verificação de nulos.
    ocorrencia.documento = "-";

    // Fazendo terceirizado pular a verificação de nulos.
    int? originalTerceirizado = ocorrencia.id_terceirizado;
    ocorrencia.id_terceirizado = 0;

    // Fazendo tipoDocumento pular a verificação de nulos.
    ocorrencia.tipoDocumento = "-";

    // Verificando se alguma das propriedades é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(ocorrencia);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Voltando terceirizado para o valor original.
    ocorrencia.id_terceirizado = originalTerceirizado;

    // Letra inicial maiúscula para o status.
    ocorrencia.status = ocorrencia.status.Substring(0, 1).ToUpper() + ocorrencia.status.Substring(1);

    // Verifica se o status passado é válido.
    if (!validStatuses.Contains(ocorrencia.status)) return Results.BadRequest("Status inválido. Status permitidos: " + string.Join(", ", validStatuses));

    // Verificando se ocorrência existe no banco de dados.
    var incident = GetOneIncidentRepository.Get(id: id, connectionString: connectionString);
    if (incident == null) return Results.NotFound("Ocorrência não encontrada.");

    // Verificando se cliente existe no banco de dados.
    var client = GetOneClientRepository.Get(id: ocorrencia.id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound("Cliente não encontrado.");

    // Verificando se veículo existe no banco de dados.
    var vehicle = GetOneVehicleRepository.Get(id: ocorrencia.id_veiculo, connectionString: connectionString);
    if (vehicle == null) return Results.NotFound("Veículo não encontrado.");

    // Verificando se veículo pertence ao cliente.
    bool veiculoIsValid = ClientVehicleValidator.Validate(id_cliente: ocorrencia.id_cliente, id_veiculo: ocorrencia.id_veiculo, connectionString: connectionString);
    if (!veiculoIsValid) return Results.BadRequest("Veículo não pertence ao cliente.");

    // Verificando se terceirizado existe no banco de dados.
    if (ocorrencia.id_terceirizado != null)
    {
      var outsourced = GetOneOutsourcedRepository.Get(id: (int)ocorrencia.id_terceirizado, connectionString: connectionString);
      if (outsourced == null) return Results.NotFound("Terceirizado não encontrado.");
    }

    // Verificando se ocorrência possui um documento. Não é possível alterar seu status para concluída sem um documento.
    var storedDocument = GetIncidentDocumentRepository.Get(id: id, connectionString: connectionString);
    if (ocorrencia.status == "Concluida" && storedDocument.documento == null) return Results.BadRequest("Esta ocorrência não possui documento. Não é possível alterar seu status para Concluída sem um documento.");

    var result = UpdateIncidentRepository.Update(incident: ocorrencia, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Ok("Ocorrência atualizada com sucesso.");
  }
}
