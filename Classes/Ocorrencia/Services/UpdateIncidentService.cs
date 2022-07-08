static class UpdateIncidentService
{

  private static string[] validStatuses = { "Andamento", "Processando", "Concluida", "Rejeitada" };
  public static IResult Update(int id, Ocorrencia ocorrencia, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

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
    bool ocorrenciaExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_ocorrencia FROM Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
    if (!ocorrenciaExists) return Results.NotFound("Ocorrência não encontrada.");

    // Verificando se cliente existe no banco de dados.
    bool clienteExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente FROM Clientes WHERE id_cliente = @Id", new { Id = ocorrencia.id_cliente });
    if (!clienteExists) return Results.NotFound("Cliente não encontrado.");

    // Verificando se veículo existe no banco de dados.
    bool veiculoExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo FROM Veiculos WHERE id_veiculo = @Id", new { Id = ocorrencia.id_veiculo });
    if (!veiculoExists) return Results.NotFound("Veículo não encontrado.");

    // Verificando se veículo pertence ao cliente.
    bool veiculoIsValid = ClientVehicleValidator.Validate(ocorrencia.id_cliente, ocorrencia.id_veiculo, dbConnectionString);
    if (!veiculoIsValid) return Results.BadRequest("Veículo não pertence ao cliente.");

    // Verificando se terceirizado existe no banco de dados.
    bool terceirizadoExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_terceirizado FROM Terceirizados WHERE id_terceirizado = @Id", new { Id = ocorrencia.id_terceirizado });
    if (!terceirizadoExists) return Results.NotFound("Terceirizado não encontrado.");

    // Verificando se ocorrência possui um documento. Não é possível alterar seu status para concluída sem um documento.
    string storedDocument = connectionString.QueryFirstOrDefault<string>("SELECT documento FROM Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
    if (ocorrencia.status == "Concluida" && storedDocument == null) return Results.BadRequest("Ocorrência não possui documento. Não é possível alterar seu status para Concluída sem um documento.");

    try
    {
      connectionString.QueryFirstOrDefault("UPDATE Ocorrencias SET data = @Data, local = @Local, UF = @UF, municipio = @Municipio, descricao = @Descricao, tipo = @Tipo, status = @Status, id_veiculo = @Id_veiculo, id_cliente = @Id_cliente, id_terceirizado = @Id_terceirizado WHERE id_ocorrencia = @Id", new { Id = id, Data = ocorrencia.data, Local = ocorrencia.local, UF = ocorrencia.UF, Municipio = ocorrencia.municipio, Descricao = ocorrencia.descricao, Tipo = ocorrencia.tipo, Status = ocorrencia.status, Id_veiculo = ocorrencia.id_veiculo, Id_cliente = ocorrencia.id_cliente, Id_terceirizado = ocorrencia.id_terceirizado });

      return Results.Ok();
    }
    catch (System.Exception)
    {

      return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");
    }

  }
}
