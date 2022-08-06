public static class UpdateCoverageService
{
  /** <summary> Esta função altera uma cobertura no banco de dados. </summary>**/
  public static IResult Update(int id, Cobertura cobertura, SqlConnection connectionString)
  {
    // Verificando se alguma das propriedades do cobertura é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificando se a cobertura existe.
    var coverage = GetOneCoverageService.Get(id: id, connectionString: connectionString);
    if (coverage == null) return Results.NotFound("Cobertura não encontrada.");

    // Verificando se o nome da cobertura já existe em outra cobertura no banco de dados.
    bool nomeAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT nome FROM Coberturas WHERE nome = @Nome AND status = 'true' AND id_cobertura != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Nome = cobertura.nome, Id = id });
    if (nomeAlreadyExists) return Results.BadRequest("O nome da cobertura já está sendo utiizado por outra cobertura ativa.");

    var result = UpdateCoverageRepository.Update(id: id, cobertura: cobertura, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Ok("Cobertura alterada com sucesso.");
  }
}
