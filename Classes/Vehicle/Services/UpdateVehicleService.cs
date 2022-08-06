public static class UpdateVehicleService
{
  /** <summary> Esta função altera um Veículo no banco de dados. </summary>**/
  public static async Task<IResult> Update(int id, Veiculo veiculo, SqlConnection connectionString)
  {
    // Verificando se veículo existe.
    var vehicle = GetOneVehicleRepository.Get(id: id, connectionString: connectionString);
    if (vehicle == null) return Results.NotFound("Veículo não encontrado.");

    // Verificando se alguma das propriedades do veiculo é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(veiculo);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificando se o RENAVAM é válido.
    bool renavamIsValid = RenavamValidator.Validate(veiculo.renavam);
    if (!renavamIsValid) return Results.BadRequest("O RENAVAM informado é inválido.");

    // Formatando modelo do veículo para passar na validação da API da FIPE.
    veiculo.modelo = VehicleModelFormatter.Format(veiculo.modelo);

    // Verificando se os dados do veículo existem na API da FIPE.
    bool veiculoIsValid = await VehicleFIPEValidator.Validate(veiculo.marca, veiculo.modelo, veiculo.ano);
    if (!veiculoIsValid) return Results.BadRequest("Este veículo não existe na tabela FIPE. Confira todos os campos e tente novamente.");

    // Verificando se o RENAVAM já existe em outro veiculo no banco de dados.
    bool renavamAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT renavam FROM Veiculos WHERE renavam = @Renavam AND id_veiculo != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Renavam = veiculo.renavam, Id = id });
    if (renavamAlreadyExists) return Results.Conflict("O RENAVAM informado já está sendo utilizado por outro veiculo.");

    // Verificando se a placa já existe em outro veiculo no banco de dados.
    bool placaAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT placa FROM Veiculos WHERE placa = @Placa AND id_veiculo != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Placa = veiculo.placa, Id = id });
    if (placaAlreadyExists) return Results.Conflict("A placa informada já está sendo utilizada por outro veiculo.");

    var result = UpdateVehicleRepository.Update(id: id, veiculo: veiculo, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Ok("Veículo alterado com sucesso.");
  }
}