public static class InsertCoverageService
{
  /** <summary> Esta função insere uma cobertura no banco de dados. </summary>**/
  public static IResult Insert(Cobertura cobertura, SqlConnection connectionString)
  {
    bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
    if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

    cobertura.status = true;

    if (cobertura.taxa_indenizacao <= 0) return Results.BadRequest(new { message = "Taxa de indenização não pode ser 0% ou menor." });

    bool nameIsValid = CoverageAlreadyExistsValidator.Validate(name: cobertura.nome, connectionString: connectionString);
    if (!nameIsValid) return Results.Conflict(new { message = "O nome da cobertura já está sendo utilizado por outra cobertura ativa." });

    var createdCoverage = InsertCoverageRepository.Insert(cobertura: cobertura, connectionString: connectionString);
    if (createdCoverage == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.Created($"/cobertura/{createdCoverage}", new { message = "Cobertura criada com sucesso.", coverage = createdCoverage });
  }
}
