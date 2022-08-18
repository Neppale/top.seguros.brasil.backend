public static class DeleteOutsourcedService
{
  /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
  public static IResult Delete(int id, SqlConnection connectionString)
  {
    var outsourced = GetOneOutsourcedRepository.Get(id: id, connectionString: connectionString);
    if (outsourced == null) return Results.NotFound(new { message = "Terceirizado não encontrado" });

    var result = DeleteOutsourcedRepository.Delete(id: id, connectionString: connectionString);
    if (result == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.NoContent();
  }
}