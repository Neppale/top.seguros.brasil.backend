using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;

public static class UpdateCoberturaService
{
  /** <summary> Esta função altera uma cobertura no banco de dados. </summary>**/
  public static IResult Update(int id, Cobertura cobertura, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do cobertura é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificando se a cobertura existe.
    bool isExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_cobertura from Coberturas WHERE id_cobertura = @Id", new { Id = id });
    if (!isExistent) return Results.NotFound("Cobertura não encontrada.");

    try
    {
      connectionString.Query("UPDATE Coberturas SET nome = @Nome, descricao = @Descricao, valor = @Valor WHERE id_cobertura = @Id", new { Nome = cobertura.nome, Descricao = cobertura.descricao, Valor = cobertura.valor, Id = id });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}
