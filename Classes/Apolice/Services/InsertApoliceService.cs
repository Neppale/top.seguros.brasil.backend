using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
static class InsertApoliceService
{
  /** <summary> Esta função insere uma apólice no banco de dados. </summary>**/
  public static IResult Insert(Apolice apolice, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do Veiculo é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(apolice);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificando valores de indenização e prêmio.
    if (apolice.indenizacao == 0) return Results.BadRequest("Valor de indenização não pode ser 0.");
    if (apolice.premio == 0) return Results.BadRequest("Valor de prêmio não pode ser 0.");

    // Por padrão, o status da apólice é "Em Análise".
    apolice.status = "Em Análise";

    // Verificando se o cliente existe no banco de dados.
    bool clienteExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente from Clientes WHERE id_cliente = @Id", new { Id = apolice.id_cliente });
    if (!clienteExists) return Results.NotFound("Cliente não encontrado.");

    // Verificando se o veículo existe no banco de dados.
    bool veiculoExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo from Veiculos WHERE id_veiculo = @Id", new { Id = apolice.id_veiculo });
    if (!veiculoExists) return Results.NotFound("Veículo não encontrado.");

    // Verificando se o veículo realmente pertence ao cliente.
    bool veiculoIsValid = ClienteVeiculoValidator.Validate(apolice.id_cliente, apolice.id_veiculo, dbConnectionString);
    if (!veiculoIsValid) return Results.BadRequest("Veículo escolhido não pertence ao cliente.");

    // Verificando se a cobertura existe no banco de dados.
    bool coberturaExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_cobertura from Coberturas WHERE id_cobertura = @Id", new { Id = apolice.id_cobertura });
    if (!coberturaExists) return Results.NotFound("Cobertura não encontrada.");

    // Verificando se usuário existe no banco de dados.
    bool usuarioExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_usuario from Usuarios WHERE id_usuario = @Id", new { Id = apolice.id_usuario });
    if (!usuarioExists) return Results.NotFound("Usuário não encontrado.");

    try
    {
      connectionString.Query("INSERT INTO Apolices (data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo) VALUES (@DataInicio, @DataFim, @Premio, @Indenizacao, @IdCobertura, @IdUsuario, @IdCliente, @IdVeiculo)", new { DataInicio = apolice.data_inicio, DataFim = apolice.data_fim, Premio = apolice.premio, Indenizacao = apolice.indenizacao, IdCobertura = apolice.id_cobertura, IdUsuario = apolice.id_usuario, IdCliente = apolice.id_cliente, IdVeiculo = apolice.id_veiculo });

      // Retornando o id da apólice inserida.
      int createdApoliceId = connectionString.QueryFirstOrDefault<int>("SELECT id_apolice FROM Apolices WHERE id_cliente = @IdCliente AND id_veiculo = @IdVeiculo AND data_inicio = @DataInicio AND data_fim = @DataFim", new { IdCliente = apolice.id_cliente, IdVeiculo = apolice.id_veiculo, DataInicio = apolice.data_inicio, DataFim = apolice.data_fim });

      return Results.Created($"/apolice/{createdApoliceId}", new { id_apolice = createdApoliceId });
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}