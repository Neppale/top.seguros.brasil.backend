static class DeleteUserService
{
  public static async Task<IResult> Delete(int id, SqlConnection connectionString)
  {
    var user = await GetUserByIdRepository.Get(id: id, connectionString: connectionString);
    if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });

    var results = await DeleteUserRepository.Delete(id: id, connectionString: connectionString);
    if (results == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde. " });

    return Results.NoContent();

  }
}