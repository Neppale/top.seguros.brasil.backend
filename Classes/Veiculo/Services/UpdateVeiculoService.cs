public static class UpdateVeiculoService
{
  /** <summary> Esta função altera um Veículo no banco de dados. </summary>**/
  public static async Task<IResult> Update(int id, Veiculo veiculo, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se veículo existe.
    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo from Veiculos WHERE id_Veiculo = @Id", new { Id = id });
    if (!Exists) return Results.NotFound("Veículo não encontrado.");

    // Verificando se alguma das propriedades do veiculo é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(veiculo);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificando se o RENAVAM é válido.
    bool RenavamIsValid = RenavamValidator.Validate(veiculo.renavam);
    if (!RenavamIsValid) return Results.BadRequest("O RENAVAM informado é inválido.");

    // Verificando se os dados do veículo existem na API da FIPE.
    bool vehicleIsValid = await VehicleFIPEValidator.Validate(veiculo.marca, veiculo.modelo, veiculo.ano);
    if (!vehicleIsValid) return Results.BadRequest("Este veículo é inválido.");

    // Verificando se o RENAVAM já existe em outro veiculo no banco de dados.
    bool renavamAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT renavam FROM Veiculos WHERE renavam = @Renavam AND id_veiculo != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Renavam = veiculo.renavam, Id = id });
    if (renavamAlreadyExists) return Results.BadRequest("O RENAVAM informado já está sendo utilizado por outro veiculo.");

    // Verificando se a placa já existe em outro veiculo no banco de dados.
    bool placaAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT placa FROM Veiculos WHERE placa = @Placa AND id_veiculo != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Placa = veiculo.placa, Id = id });
    if (placaAlreadyExists) return Results.BadRequest("A placa informada já está sendo utilizada por outro veiculo.");

    try
    {
      connectionString.Query("UPDATE Veiculos SET marca = @Marca, modelo = @Modelo, ano = @Ano, uso = @Uso, placa = @Placa, renavam = @Renavam, sinistrado = @Sinistrado, id_cliente = @IdCliente WHERE id_Veiculo = @Id", new { Marca = veiculo.marca, Modelo = veiculo.modelo, Ano = veiculo.ano, Uso = veiculo.uso, Placa = veiculo.placa, Renavam = veiculo.renavam, Sinistrado = veiculo.sinistrado, IdCliente = veiculo.id_cliente, Id = id });

      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}