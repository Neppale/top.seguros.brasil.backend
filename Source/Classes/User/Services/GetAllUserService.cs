static class GetAllUserService
{
  /** <summary> Esta função retorna todos os usuários no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber)
  {
    if (pageNumber == null) pageNumber = 1;

    var data = GetAllUserRepository.Get(connectionString: connectionString, pageNumber: pageNumber);
    return Results.Ok(data);
  }
}