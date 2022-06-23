using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;

public static class InsertCoberturaService
{
  /** <summary> Esta função insere uma cobertura no banco de dados. </summary>**/
  public static IResult Insert(Cobertura cobertura, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades do Cobertura é nula ou vazia.
      bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      connectionString.Query<Cobertura>("INSERT INTO Coberturas (nome, descricao, valor, status) VALUES (@Nome, @Descricao, @Valor, @Status)", new { Nome = cobertura.nome, Descricao = cobertura.descricao, Valor = cobertura.valor, Status = cobertura.status });

      // Pegando o ID da cobertura que acabou de ser inserida.
      int createdCoberturaId = connectionString.QueryFirstOrDefault<int>("SELECT id_cobertura FROM Coberturas WHERE nome = @Nome", new { Nome = cobertura.nome });

      return Results.Created($"/cobertura/{createdCoberturaId}", new { id_cobertura = createdCoberturaId });
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}
