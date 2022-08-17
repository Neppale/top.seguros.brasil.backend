static class GetAllPolicyService
{
  /** <summary> Esta função retorna as apólices no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber)
  {
    if (pageNumber == null) pageNumber = 1;

    var result = GetAllPolicyRepository.Get(connectionString, pageNumber);

    return Results.Ok(result);
  }
}