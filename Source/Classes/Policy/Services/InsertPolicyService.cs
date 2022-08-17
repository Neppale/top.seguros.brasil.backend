static class InsertPolicyService
{
  /** <summary> Esta função insere uma apólice no banco de dados. </summary>**/
  public static async Task<IResult> Insert(Apolice apolice, SqlConnection connectionString)
  {
    bool hasValidProperties = NullPropertyValidator.Validate(apolice);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    apolice.status = "Em Análise";

    if (apolice.indenizacao <= 0) return Results.BadRequest("Valor de indenização não pode ser menor ou igual a zero.");
    if (apolice.premio <= 0) return Results.BadRequest("Valor de prêmio não pode ser menor ou igual a zero.");

    var cliente = GetOneClientRepository.Get(id: apolice.id_cliente, connectionString: connectionString);
    if (cliente == null) return Results.NotFound("Cliente não encontrado.");

    var vehicle = GetOneVehicleRepository.Get(id: apolice.id_veiculo, connectionString: connectionString);
    if (vehicle == null) return Results.NotFound("Veículo não encontrado.");

    bool vehicleBelongsToClient = ClientVehicleValidator.Validate(id_cliente: apolice.id_cliente, id_veiculo: apolice.id_veiculo, connectionString: connectionString);
    if (!vehicleBelongsToClient) return Results.BadRequest("Veículo não pertence ao cliente.");

    var coverage = GetOneCoverageRepository.Get(id: apolice.id_cobertura, connectionString: connectionString);
    if (coverage == null) return Results.NotFound("Cobertura não encontrada.");

    var user = GetOneUserRepository.Get(id: apolice.id_usuario, connectionString: connectionString);
    if (user == null) return Results.NotFound("Usuário não encontrado.");

    var result = await InsertPolicyRepository.Insert(apolice: apolice, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/apolice/{result}", new { id_apolice = result });
  }
}
