public static class UpdateCoverageService
{
  /** <summary> Esta função altera uma cobertura no banco de dados. </summary>**/
  public static IResult Update(int id, Cobertura cobertura, SqlConnection connectionString)
  {
    bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
    if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

    var coverage = GetOneCoverageService.Get(id: id, connectionString: connectionString);
    if (coverage == null) return Results.NotFound("Cobertura não encontrada.");

    bool nameIsValid = CoverageAlreadyExistsValidator.Validate(id: id, name: cobertura.nome, connectionString: connectionString);
    if (!nameIsValid) return Results.BadRequest("O nome da cobertura já está sendo utiizado por outra cobertura ativa.");

    var result = UpdateCoverageRepository.Update(id: id, cobertura: cobertura, connectionString: connectionString);
    if (result == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.Ok("Cobertura alterada com sucesso.");
  }
}
