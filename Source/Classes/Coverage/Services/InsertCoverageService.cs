public static class InsertCoverageService
{
  /** <summary> Esta função insere uma cobertura no banco de dados. </summary>**/
  public static IResult Insert(Cobertura cobertura, SqlConnection connectionString)
  {
    // Verificando se alguma das propriedades do Cobertura é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificando se valor de indenização é maior que 0.
    if (cobertura.taxa_indenizacao <= 0) return Results.BadRequest("Taxa de indenização não pode ser 0% ou menor.");

    // Por padrão, o status da cobertura é true.
    cobertura.status = true;

    // Verificando se o nome da cobertura já existe em outra cobertura no banco de dados.
    bool nameIsValid = CoverageAlreadyExistsValidator.Validate(name: cobertura.nome, connectionString: connectionString);
    if (!nameIsValid) return Results.Conflict("O nome da cobertura já está sendo utiizado por outra cobertura ativa.");

    var result = InsertCoverageRepository.Insert(cobertura: cobertura, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/cobertura/{result}", new { id_cobertura = result });

  }
}
