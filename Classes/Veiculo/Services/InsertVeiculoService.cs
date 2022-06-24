public static class InsertVeiculoService
{
  /** <summary> Esta função insere um Veiculo no banco de dados. </summary>**/
  public static IResult Insert(Veiculo veiculo, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do Veiculo é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(veiculo);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Por padrão, o status do veículo é true.
    veiculo.status = true;

    // Verificando se o RENAVAM é válido.
    bool RenavamIsValid = RenavamValidator.Validate(veiculo.renavam);
    if (!RenavamIsValid) return Results.BadRequest("O RENAVAM informado é inválido.");

    // Verificando se o RENAVAM já existe em outro veiculo no banco de dados.
    bool renavamAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT renavam FROM Veiculos WHERE renavam = @Renavam) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Renavam = veiculo.renavam });
    if (renavamAlreadyExists) return Results.BadRequest("O RENAVAM informado já está sendo utilizado por outro veiculo.");

    // Verificando se a placa já existe em outro veiculo no banco de dados.
    bool placaAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT placa FROM Veiculos WHERE placa = @Placa) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Placa = veiculo.placa });
    if (placaAlreadyExists) return Results.BadRequest("A placa informada já está sendo utilizada por outro veiculo.");

    // Verificando se o cliente existe.
    bool clienteIsValid = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente from Clientes WHERE id_cliente = @Id", new { Id = veiculo.id_cliente });
    if (!clienteIsValid) return Results.BadRequest("Cliente não encontrado.");

    try
    {
      connectionString.Query<Veiculo>("INSERT INTO Veiculos (marca, modelo, ano, uso, placa, renavam, sinistrado, id_cliente) VALUES (@Marca, @Modelo, @Ano, @Uso, @Placa, @Renavam, @Sinistrado, @IdCliente)", new { Marca = veiculo.marca, Modelo = veiculo.modelo, Ano = veiculo.ano, Uso = veiculo.uso, Placa = veiculo.placa, Renavam = veiculo.renavam, Sinistrado = veiculo.sinistrado, IdCliente = veiculo.id_cliente });

      // Retornando o id do veículo criado.
      int createdVeiculoId = connectionString.QueryFirstOrDefault<int>("SELECT id_veiculo FROM Veiculos WHERE renavam = @Renavam", new { Renavam = veiculo.renavam });

      return Results.Created($"/veiculo/{createdVeiculoId}", new { id_veiculo = createdVeiculoId });
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}