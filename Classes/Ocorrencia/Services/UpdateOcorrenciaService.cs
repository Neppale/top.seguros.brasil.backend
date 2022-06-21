using Dapper;
using Microsoft.Data.SqlClient;
static class UpdateOcorrenciaService
{
  public static IResult Update(int id, Ocorrencia ocorrencia, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se ocorrência existe no banco de dados.
    bool ocorrenciaIsExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_ocorrencia FROM Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
    if (!ocorrenciaIsExistent) return Results.NotFound("Ocorrência não encontrada.");

    // Verificando se cliente existe no banco de dados.
    bool clienteIsExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente FROM Clientes WHERE id_cliente = @Id", new { Id = ocorrencia.id_cliente });
    if (!clienteIsExistent) return Results.NotFound("Cliente não encontrado.");

    // Verificando se veículo existe no banco de dados.
    bool veiculoIsExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo FROM Veiculos WHERE id_veiculo = @Id", new { Id = ocorrencia.id_veiculo });
    if (!veiculoIsExistent) return Results.NotFound("Veículo não encontrado.");

    // Verificando se veículo pertence ao cliente.
    bool veiculoIsValid = ClienteVeiculoValidator.Validate(ocorrencia.id_cliente, ocorrencia.id_veiculo, dbConnectionString);
    if (!veiculoIsValid) return Results.BadRequest("Veículo não pertence ao cliente.");

    // Verificando se terceirizado existe no banco de dados.
    bool terceirizadoIsExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_terceirizado FROM Terceirizados WHERE id_terceirizado = @Id", new { Id = ocorrencia.id_terceirizado });
    if (!terceirizadoIsExistent) return Results.NotFound("Terceirizado não encontrado.");

    try
    {
      connectionString.QueryFirstOrDefault("UPDATE Ocorrencias SET data = @Data, local = @Local, UF = @UF, municipio = @Municipio, descricao = @Descricao, tipo = @Tipo, status = @Status, id_veiculo = @Id_veiculo, id_cliente = @Id_cliente, id_terceirizado = @Id_terceirizado WHERE id_ocorrencia = @Id", new { Id = id, Data = ocorrencia.data, Local = ocorrencia.local, UF = ocorrencia.UF, Municipio = ocorrencia.municipio, Descricao = ocorrencia.descricao, Tipo = ocorrencia.tipo, Status = ocorrencia.status, Id_veiculo = ocorrencia.id_veiculo, Id_cliente = ocorrencia.id_cliente, Id_terceirizado = ocorrencia.id_terceirizado });

      return Results.Ok();
    }
    catch (System.Exception)
    {

      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}
