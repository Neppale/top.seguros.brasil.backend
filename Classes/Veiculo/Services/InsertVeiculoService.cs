using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;

public static class InsertVeiculoService
{
  /** <summary> Esta função insere um Veiculo no banco de dados. </summary>**/
  public static IResult Insert(Veiculo veiculo, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades do Veiculo é nula ou vazia.
      bool hasValidProperties = NullPropertyValidator.Validate(veiculo);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      bool RenavamIsValid = RenavamValidator.Validate(veiculo.renavam);
      if (!RenavamIsValid) return Results.BadRequest("O RENAVAM informado é inválido.");

      bool clienteIsValid = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente from Clientes WHERE id_cliente = @Id", new { Id = veiculo.id_cliente });
      if (!clienteIsValid) return Results.BadRequest("Cliente não encontrado.");

      connectionString.Query<Veiculo>("INSERT INTO Veiculos (marca, modelo, ano, uso, placa, renavam, sinistrado, id_cliente) VALUES (@Marca, @Modelo, @Ano, @Uso, @Placa, @Renavam, @Sinistrado, @IdCliente)", new { Marca = veiculo.marca, Modelo = veiculo.modelo, Ano = veiculo.ano, Uso = veiculo.uso, Placa = veiculo.placa, Renavam = veiculo.renavam, Sinistrado = veiculo.sinistrado, IdCliente = veiculo.id_cliente });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}