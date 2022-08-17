public static class DeleteOutsourcedService
{
  /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
  public static IResult Delete(int id, SqlConnection connectionString)
  {
    // Verificando se terceirizado existe.
    var outsourced = GetOneOutsourcedRepository.Get(id: id, connectionString: connectionString);
    if (outsourced == null) return Results.NotFound("Terceirizado não encontrado");

    var result = DeleteOutsourcedRepository.Delete(id: id, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.NoContent();
  }
}