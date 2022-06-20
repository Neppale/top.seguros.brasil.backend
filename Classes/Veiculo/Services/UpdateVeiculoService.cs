using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;

public static class UpdateVeiculoService
{
  /** <summary> Esta função altera um Veículo no banco de dados. </summary>**/
  public static IResult Update(int id, Veiculo veiculo, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se veículo existe.
    bool isExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo from Veiculos WHERE id_Veiculo = @Id", new { Id = id });
    if (!isExistent) return Results.NotFound("Veículo não encontrado.");

    // Verificando se alguma das propriedades do veiculo é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(veiculo);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    bool RenavamIsValid = RenavamValidator.Validate(veiculo.renavam);
    if (!RenavamIsValid) return Results.BadRequest("O RENAVAM informado é inválido.");

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