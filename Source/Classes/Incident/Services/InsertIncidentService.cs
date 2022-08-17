static class InsertIncidentService
{
  /** <summary> Esta função insere uma ocorrência no banco de dados. </summary>**/
  public static IResult Insert(Ocorrencia ocorrencia, SqlConnection connectionString)
  {

    int? originalId_Terceirizado = ocorrencia.id_terceirizado;
    if (ocorrencia.id_terceirizado == null) ocorrencia.id_terceirizado = 0;

    string? originalDocumento = ocorrencia.documento;
    if (ocorrencia.documento == null || ocorrencia.documento == "") ocorrencia.documento = "-";

    string? originalTipoDocumento = ocorrencia.tipoDocumento;
    if (ocorrencia.tipoDocumento == null || ocorrencia.tipoDocumento == "") ocorrencia.tipoDocumento = "-";

    if (ocorrencia.status == null) ocorrencia.status = "Andamento";

    bool hasValidProperties = NullPropertyValidator.Validate(ocorrencia);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    bool dateIsValid = IncidentDateValidator.Validate(ocorrencia.data);
    if (!dateIsValid) return Results.BadRequest("A data da ocorrência não pode ser maior que a data atual.");

    ocorrencia.data = SqlDateConverter.Convert(ocorrencia.data);
    ocorrencia.id_terceirizado = originalId_Terceirizado;
    ocorrencia.tipoDocumento = originalTipoDocumento;
    ocorrencia.documento = originalDocumento;

    var client = GetOneClientRepository.Get(id: ocorrencia.id_cliente, connectionString);
    if (client == null) return Results.NotFound("Cliente não encontrado.");

    var vehicle = GetOneVehicleRepository.Get(id: ocorrencia.id_veiculo, connectionString);
    if (vehicle == null) return Results.NotFound("Veículo não encontrado.");

    bool vehicleIsValid = ClientVehicleValidator.Validate(id_cliente: ocorrencia.id_cliente, id_veiculo: ocorrencia.id_veiculo, connectionString: connectionString);
    if (!vehicleIsValid) return Results.BadRequest("Veículo não pertence ao cliente.");

    var result = InsertIncidentRepository.Insert(incident: ocorrencia, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/ocorrencia/{result}", new { id_ocorrencia = result });
  }
}