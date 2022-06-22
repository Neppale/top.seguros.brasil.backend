using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
static class InsertOcorrenciaService
{
  /** <summary> Esta função insere uma ocorrência no banco de dados. </summary>**/
  public static IResult Insert(Ocorrencia ocorrencia, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {

      // Fazendo o terceirizado pular a validação.
      if (ocorrencia.id_terceirizado == null) ocorrencia.id_terceirizado = 0;

      // Fazendo o documento pular a validação.
      if (ocorrencia.documento == "" || ocorrencia.documento == null) ocorrencia.documento = "-";

      // Verificando se alguma das propriedades da ocorrencia é nula ou vazia.
      bool hasValidProperties = NullPropertyValidator.Validate(ocorrencia);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      // Voltando terceirizado para o valor original.
      ocorrencia.id_terceirizado = ocorrencia.id_terceirizado = null;

      // Voltando documento para o valor original.
      ocorrencia.documento = ocorrencia.documento.Replace("-", "");

      // Verificando se cliente existe no banco de dados.
      bool clienteIsExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente FROM Clientes WHERE id_cliente = @Id", new { Id = ocorrencia.id_cliente });
      if (!clienteIsExistent) return Results.NotFound("Cliente não encontrado.");

      // Verificando se veículo existe no banco de dados.
      bool veiculoIsExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo FROM Veiculos WHERE id_veiculo = @Id", new { Id = ocorrencia.id_veiculo });
      if (!veiculoIsExistent) return Results.NotFound("Veículo não encontrado.");

      // Verificando se veículo pertence ao cliente.
      bool veiculoIsValid = ClienteVeiculoValidator.Validate(ocorrencia.id_cliente, ocorrencia.id_veiculo, dbConnectionString);
      if (!veiculoIsValid) return Results.BadRequest("Veículo não pertence ao cliente.");

      connectionString.Query<Veiculo>("INSERT INTO Ocorrencias (data, local, UF, municipio, descricao, tipo, id_veiculo, id_cliente, id_terceirizado) VALUES (@Data, @Local, @UF, @Municipio, @Descricao, @Tipo,  @IdVeiculo, @IdCliente, @IdTerceirizado)", new { Data = ocorrencia.data, Local = ocorrencia.local, UF = ocorrencia.UF, Municipio = ocorrencia.municipio, Descricao = ocorrencia.descricao, Tipo = ocorrencia.tipo, IdVeiculo = ocorrencia.id_veiculo, IdCliente = ocorrencia.id_cliente, IdTerceirizado = ocorrencia.id_terceirizado });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}