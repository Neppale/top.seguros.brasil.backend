static class DeleteClientService
{
  public static IResult Delete(int id, SqlConnection connectionString)
  {
    var client = GetClientByIdRepository.Get(id: id, connectionString: connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    var result = DeleteClientRepository.Delete(id: id, connectionString: connectionString);
    if (result == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.NoContent();
  }
}