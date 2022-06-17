using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
abstract class InsertOcorrenciaService
{
  /** <summary> Esta função insere uma ocorrência no banco de dados. </summary>**/
  public static IResult Insert(Ocorrencia ocorrencia, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades da ocorrencia é nula ou vazia.
      //TODO: Terceirizado e documento pode ser vazio. Não deve passar por essa validação.
      bool hasValidProperties = NullPropertyValidator.Validate(ocorrencia);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      // Verificando se cliente existe no banco de dados.
      bool clienteIsExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente FROM Clientes WHERE id_cliente = @Id", new { Id = ocorrencia.id_cliente });
      if (!clienteIsExistent) return Results.NotFound("Cliente não encontrado.");

      // Verificando se veículo existe no banco de dados.
      bool veiculoIsExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo FROM Veiculos WHERE id_veiculo = @Id", new { Id = ocorrencia.id_veiculo });
      if (!veiculoIsExistent) return Results.NotFound("Veículo não encontrado.");

      var data = connectionString.Query<Veiculo>("INSERT INTO Ocorrencias (data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado) VALUES (@Data, @Local, @UF, @Municipio, @Descricao, @Tipo, @Status, @IdVeiculo, @IdCliente, @IdTerceirizado)", new { Data = ocorrencia.data, Local = ocorrencia.local, UF = ocorrencia.UF, Municipio = ocorrencia.municipio, Descricao = ocorrencia.descricao, Tipo = ocorrencia.tipo, Status = ocorrencia.status, IdVeiculo = ocorrencia.id_veiculo, IdCliente = ocorrencia.id_cliente, IdTerceirizado = ocorrencia.id_terceirizado });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}