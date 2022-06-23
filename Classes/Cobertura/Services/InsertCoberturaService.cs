using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;

public static class InsertCoberturaService
{
  /** <summary> Esta função insere uma cobertura no banco de dados. </summary>**/
  public static IResult Insert(Cobertura cobertura, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);


    // Verificando se alguma das propriedades do Cobertura é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificando se o nome da cobertura já existe em outra cobertura no banco de dados.
    bool nomeAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT nome FROM Coberturas WHERE nome = @Nome AND status = 'true') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Nome = cobertura.nome });
    if (nomeAlreadyExists) return Results.BadRequest("O nome da cobertura já está sendo utiizado por outra cobertura ativa.");

    try
    {
      connectionString.Query<Cobertura>("INSERT INTO Coberturas (nome, descricao, valor) VALUES (@Nome, @Descricao, @Valor)", new { Nome = cobertura.nome, Descricao = cobertura.descricao, Valor = cobertura.valor });

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
