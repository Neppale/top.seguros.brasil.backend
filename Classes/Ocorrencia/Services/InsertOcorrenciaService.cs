static class InsertOcorrenciaService
{
  /** <summary> Esta função insere uma ocorrência no banco de dados. </summary>**/
  public static IResult Insert(Ocorrencia ocorrencia, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Fazendo o id_terceirizado pular a verificação de nulos.
    int? originalId_Terceirizado = ocorrencia.id_terceirizado;
    if (ocorrencia.id_terceirizado == null) ocorrencia.id_terceirizado = 0;

    // Fazendo o documento pular a verificação de nulos.
    string? originalDocumento = ocorrencia.documento;
    if (ocorrencia.documento == null || ocorrencia.documento == "") ocorrencia.documento = "-";

    // Por padrão, o status é "Andamento".
    if (ocorrencia.status == null) ocorrencia.status = "Andamento";

    // Verificando se alguma das propriedades da ocorrência é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(ocorrencia);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Voltando terceirizado para o valor original.
    ocorrencia.id_terceirizado = originalId_Terceirizado;

    // Voltando documento para o valor original.
    ocorrencia.documento = originalDocumento;

    // Verificando se cliente existe no banco de dados.
    bool clienteExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente FROM Clientes WHERE id_cliente = @Id", new { Id = ocorrencia.id_cliente });
    if (!clienteExists) return Results.NotFound("Cliente não encontrado.");

    // Verificando se veículo existe no banco de dados.
    bool veiculoExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo FROM Veiculos WHERE id_veiculo = @Id", new { Id = ocorrencia.id_veiculo });
    if (!veiculoExists) return Results.NotFound("Veículo não encontrado.");

    // Verificando se veículo pertence ao cliente.
    bool veiculoIsValid = ClienteVeiculoValidator.Validate(ocorrencia.id_cliente, ocorrencia.id_veiculo, dbConnectionString);
    if (!veiculoIsValid) return Results.BadRequest("Veículo não pertence ao cliente.");

    try
    {
      connectionString.Query<Veiculo>("INSERT INTO Ocorrencias (data, local, UF, municipio, descricao, tipo, id_veiculo, id_cliente, id_terceirizado) VALUES (@Data, @Local, @UF, @Municipio, @Descricao, @Tipo,  @IdVeiculo, @IdCliente, @IdTerceirizado)", new { Data = ocorrencia.data, Local = ocorrencia.local, UF = ocorrencia.UF, Municipio = ocorrencia.municipio, Descricao = ocorrencia.descricao, Tipo = ocorrencia.tipo, IdVeiculo = ocorrencia.id_veiculo, IdCliente = ocorrencia.id_cliente, IdTerceirizado = ocorrencia.id_terceirizado });

      // Pegando o id da ocorrência inserida para retornar na resposta.
      int createdOcorrenciaId = connectionString.QueryFirstOrDefault<int>("SELECT id_ocorrencia FROM Ocorrencias WHERE data = @Data AND local = @Local AND UF = @UF AND municipio = @Municipio AND descricao = @Descricao AND tipo = @Tipo AND id_veiculo = @IdVeiculo AND id_cliente = @IdCliente AND id_terceirizado = @IdTerceirizado", new { Data = ocorrencia.data, Local = ocorrencia.local, UF = ocorrencia.UF, Municipio = ocorrencia.municipio, Descricao = ocorrencia.descricao, Tipo = ocorrencia.tipo, IdVeiculo = ocorrencia.id_veiculo, IdCliente = ocorrencia.id_cliente, IdTerceirizado = ocorrencia.id_terceirizado });

      return Results.Created($"/ocorrencia/{createdOcorrenciaId}", new { id_ocorrencia = createdOcorrenciaId });
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}