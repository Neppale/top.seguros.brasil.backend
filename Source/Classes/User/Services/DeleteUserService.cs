static class DeleteUserService
{
  public static IResult Delete(int id, SqlConnection connectionString)
  {
    var user = GetOneUserRepository.Get(id: id, connectionString: connectionString);
    if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });

    var results = DeleteUserRepository.Delete(id: id, connectionString: connectionString);
    if (results == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde. " });

    return Results.NoContent();

  }
}