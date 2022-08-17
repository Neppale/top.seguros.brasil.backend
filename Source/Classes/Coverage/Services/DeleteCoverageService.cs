public static class DeleteCoverageService
{
  /** <summary> Esta função desativa uma cobertura no banco de dados. </summary>**/
  public static IResult Delete(int id, SqlConnection connectionString)
  {

    // Verificando se a cobertura existe.
    var coverage = GetOneCoverageRepository.Get(id: id, connectionString: connectionString);
    if (coverage == null) return Results.NotFound("Cobertura não encontrada.");

    var result = DeleteCoverageRepository.Delete(id: id, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.NoContent();
  }
}