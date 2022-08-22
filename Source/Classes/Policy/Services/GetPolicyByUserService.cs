static class GetPolicyByUserService
{
  public static IResult Get(int id_usuario, int? pageNumber, SqlConnection connectionString)
  {

    var user = GetUserByIdRepository.Get(id: id_usuario, connectionString: connectionString);
    if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });

    if (pageNumber == null) pageNumber = 1;
    var data = GetPolicyByUserRepository.Get(id: id_usuario, connectionString: connectionString, pageNumber: pageNumber);
    if (data.Count() == 0) return Results.NotFound(new { message = "Nenhuma apólice encontrada para o usuário." });

    return Results.Ok(data);
  }
}
