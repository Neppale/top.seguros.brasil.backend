static class GetOnePolicyService
{
  /** <summary> Esta função retorna uma apólice específica no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var policy = GetOnePolicyRepository.Get(id, connectionString);
    if (policy == null) return Results.NotFound(new { message = "Apólice não encontrada." });

    return Results.Ok(policy);
  }
}

