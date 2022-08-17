static class DeleteUserService
{
  public static IResult Delete(int id, SqlConnection connectionString)
  {
    var user = GetOneUserRepository.Get(id: id, connectionString: connectionString);
    if (user == null) return Results.NotFound("Usuário não encontrado.");

    var results = DeleteUserRepository.Delete(id: id, connectionString: connectionString);
    if (results == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde. ");

    return Results.NoContent();

  }
}