static class GetOneUserService
{
  /** <summary> Esta função retorna um usuário específico no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var user = GetOneUserRepository.Get(id: id, connectionString: connectionString);
    if (user == null) return Results.NotFound("Usuário não encontrado.");

    return Results.Ok(user);
  }
}

