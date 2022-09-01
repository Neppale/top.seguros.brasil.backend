static class GetAllPolicyService
{
  /** <summary> Esta função retorna as apólices no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    if (pageNumber == null) pageNumber = 1;
    if (size == null) size = 5;

    var result = GetAllPolicyRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);

    return Results.Ok(result);
  }
}