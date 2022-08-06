static class DeleteClientService
{
  public static IResult Delete(int id, SqlConnection connectionString)
  {
    // Verificando se o cliente existe.
    var client = GetOneClientRepository.Get(id: id, connectionString: connectionString);
    if (client == null) return Results.NotFound("Cliente não encontrado.");

    var result = DeleteClientRepository.Delete(id: id, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.NoContent();
  }
}