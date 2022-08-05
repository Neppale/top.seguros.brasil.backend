public static class InsertCoverageService
{
  /** <summary> Esta função insere uma cobertura no banco de dados. </summary>**/
  public static IResult Insert(Cobertura cobertura, SqlConnection connectionString)
  {
    // Verificando se alguma das propriedades do Cobertura é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Por padrão, o status da cobertura é true.
    cobertura.status = true;

    // Verificando se o nome da cobertura já existe em outra cobertura no banco de dados.
    bool nomeAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT nome FROM Coberturas WHERE nome = @Nome AND status = 'true') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Nome = cobertura.nome });
    if (nomeAlreadyExists) return Results.Conflict("O nome da cobertura já está sendo utiizado por outra cobertura ativa.");

    var result = InsertCoverageRepository.Insert(cobertura: cobertura, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/cobertura/{result}", new { id_cobertura = result });

  }
}
