static class InsertPolicyService
{
  /** <summary> Esta função insere uma apólice no banco de dados. </summary>**/
  public static async Task<IResult> Insert(Apolice apolice, SqlConnection connectionString)
  {
    apolice.documento = "-";

    bool hasValidProperties = NullPropertyValidator.Validate(apolice);
    if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

    apolice.status = "Em Analise";

    if (apolice.indenizacao <= 0) return Results.BadRequest(new { message = "Valor de indenização não pode ser menor ou igual a zero." });
    if (apolice.premio <= 0) return Results.BadRequest(new { message = "Valor de prêmio não pode ser menor ou igual a zero." });

    var cliente = GetClientByIdRepository.Get(id: apolice.id_cliente, connectionString: connectionString);
    if (cliente == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    var vehicle = GetVehicleByIdRepository.Get(id: apolice.id_veiculo, connectionString: connectionString);
    if (vehicle == null) return Results.NotFound(new { message = "Veículo não encontrado." });

    bool vehicleBelongsToClient = ClientVehicleValidator.Validate(id_cliente: apolice.id_cliente, id_veiculo: apolice.id_veiculo, connectionString: connectionString);
    if (!vehicleBelongsToClient) return Results.BadRequest(new { message = "Veículo não pertence ao cliente." });

    var coverage = GetCoverageByIdRepository.Get(id: apolice.id_cobertura, connectionString: connectionString);
    if (coverage == null) return Results.NotFound(new { message = "Cobertura não encontrada." });

    var user = GetUserByIdRepository.Get(id: apolice.id_usuario, connectionString: connectionString);
    if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });

    var createdPolicy = await InsertPolicyRepository.Insert(apolice: apolice, connectionString: connectionString);
    if (createdPolicy == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });
    if (createdPolicy.id_apolice == 0) return Results.Created($"/apolice/{createdPolicy.id_apolice}", new { message = "Apólice criada com sucesso, mas não foi possível criar o documento. Tente novamente mais tarde." });

    return Results.Created($"/apolice/{createdPolicy.id_apolice}", new { message = "Apólice criada com sucesso.", policy = createdPolicy });
  }
}
