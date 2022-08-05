static class InsertIncidentService
{
  /** <summary> Esta função insere uma ocorrência no banco de dados. </summary>**/
  public static IResult Insert(Ocorrencia ocorrencia, SqlConnection connectionString)
  {
    // Fazendo o id_terceirizado pular a verificação de nulos.
    int? originalId_Terceirizado = ocorrencia.id_terceirizado;
    if (ocorrencia.id_terceirizado == null) ocorrencia.id_terceirizado = 0;

    // Fazendo o documento pular a verificação de nulos.
    string? originalDocumento = ocorrencia.documento;
    if (ocorrencia.documento == null || ocorrencia.documento == "") ocorrencia.documento = "-";

    // Fazendo o tipoDocumento pular a verificação de nulos.
    string? originalTipoDocumento = ocorrencia.tipoDocumento;
    if (ocorrencia.tipoDocumento == null || ocorrencia.tipoDocumento == "") ocorrencia.tipoDocumento = "-";

    // Por padrão, o status é "Andamento".
    if (ocorrencia.status == null) ocorrencia.status = "Andamento";

    // Verificando se alguma das propriedades da ocorrência é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(ocorrencia);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Voltando terceirizado para o valor original.
    ocorrencia.id_terceirizado = originalId_Terceirizado;

    // Voltando documento para o valor original.
    ocorrencia.documento = originalDocumento;

    // Voltando tipoDocumento para o valor original.
    ocorrencia.tipoDocumento = originalTipoDocumento;

    // Verificando se cliente existe no banco de dados.
    var client = GetOneClientRepository.Get(id: ocorrencia.id_cliente, connectionString);
    if (client == null) return Results.NotFound("Cliente não encontrado.");

    // Verificando se veículo existe no banco de dados.
    var vehicle = GetOneVehicleRepository.Get(id: ocorrencia.id_veiculo, connectionString);
    if (vehicle == null) return Results.NotFound("Veículo não encontrado.");

    // Verificando se veículo pertence ao cliente.
    bool veiculoIsValid = ClientVehicleValidator.Validate(id_cliente: ocorrencia.id_cliente, id_veiculo: ocorrencia.id_veiculo, connectionString: connectionString);
    if (!veiculoIsValid) return Results.BadRequest("Veículo não pertence ao cliente.");

    var result = InsertIncidentRepository.Insert(incident: ocorrencia, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Ocorreu um erro ao inserir a ocorrência.");

    return Results.Created($"/ocorrencia/{result}", new { id_ocorrencia = result });
  }
}