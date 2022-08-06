static class GetAllPolicyService
{
  /** <summary> Esta função retorna as apólices no banco de dados. </summary>**/
  public static IResult Get(SqlConnection connectionString, int? pageNumber)
  {
    // se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    var result = GetAllPolicyRepository.Get(connectionString, pageNumber);

    return Results.Ok(result);
  }
}