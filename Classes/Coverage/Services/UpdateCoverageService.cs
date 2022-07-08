public static class UpdateCoverageService
{
  /** <summary> Esta função altera uma cobertura no banco de dados. </summary>**/
  public static IResult Update(int id, Cobertura cobertura, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do cobertura é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificando se a cobertura existe.
    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_cobertura from Coberturas WHERE id_cobertura = @Id", new { Id = id });
    if (!Exists) return Results.NotFound("Cobertura não encontrada.");

    // Verificando se o nome da cobertura já existe em outra cobertura no banco de dados.
    bool nomeAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT nome FROM Coberturas WHERE nome = @Nome AND status = 'true' AND id_cobertura != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Nome = cobertura.nome, Id = id });
    if (nomeAlreadyExists) return Results.BadRequest("O nome da cobertura já está sendo utiizado por outra cobertura ativa.");

    try
    {
      connectionString.Query("UPDATE Coberturas SET nome = @Nome, descricao = @Descricao, valor = @Valor, taxa_indenizacao = @TaxaIndenizacao WHERE id_cobertura = @Id", new { Nome = cobertura.nome, Descricao = cobertura.descricao, Valor = cobertura.valor, Id = id, TaxaIndenizacao = cobertura.taxa_indenizacao });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");
    }

  }
}
