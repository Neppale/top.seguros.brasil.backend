static class GenerateApoliceService
{

  public static async Task<IResult> Generate(int id_cliente, int id_veiculo, int id_cobertura, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se cliente existe e está ativo.
    bool clienteExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente from Clientes WHERE id_Cliente = @Id AND status = 'true'", new { Id = id_cliente });
    if (!clienteExists) return Results.BadRequest("Cliente não encontrado.");

    // Verificando se veiculo existe e está ativo.
    bool veiculoExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo from Veiculos WHERE id_veiculo = @Id AND status = 'true'", new { Id = id_veiculo });
    if (!veiculoExists) return Results.BadRequest("Veiculo não encontrado.");

    // Verificando se cobertura existe e está ativa.
    bool coberturaExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_cobertura from Coberturas WHERE id_cobertura = @Id AND status = 'true'", new { Id = id_cobertura });
    if (!coberturaExists) return Results.BadRequest("Cobertura não encontrada.");

    // Verificando se o veículo realmente pertence ao cliente.
    bool veiculoBelongsToCliente = ClienteVeiculoValidator.Validate(id_cliente, id_veiculo, dbConnectionString);
    if (!veiculoBelongsToCliente) return Results.BadRequest("Veículo escolhido não pertence ao cliente.");

    try
    {
      Apolice generatedApolice = new();
      generatedApolice.data_inicio = DateTime.Now.ToString();
      generatedApolice.data_inicio = generatedApolice.data_inicio.Substring(0, 10) + " 00:00:00"; // Removendo as horas da data de ínicio.
      generatedApolice.data_fim = DateTime.Now.AddYears(1).ToString();
      generatedApolice.data_fim = generatedApolice.data_fim.Substring(0, 10) + " 00:00:00"; // Removendo as horas da data de fim.
      generatedApolice.indenizacao = await IndemnisationGenerator.Generate(id_veiculo, dbConnectionString);
      generatedApolice.premio = await PremiumGenerator.Generate(id_veiculo, id_cobertura, dbConnectionString);
      generatedApolice.id_cliente = id_cliente;
      generatedApolice.id_cobertura = id_cobertura;
      generatedApolice.id_veiculo = id_veiculo;
      generatedApolice.id_usuario = UsuarioSelector.Select(dbConnectionString);
      generatedApolice.status = "Em Análise";

      return Results.Ok(new { message = "Apólice gerada com sucesso.", warning = "Lembre-se de que a apólice não foi salva no banco de dados, apenas gerada, e por isso seu id é 0.", apolice = generatedApolice });
    }
    catch (SystemException)
    {
      return Results.BadRequest("Erro ao gerar apólice. Tente novamente mais tarde.");
    }

  }
}