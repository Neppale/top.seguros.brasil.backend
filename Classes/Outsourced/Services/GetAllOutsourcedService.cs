public static class GetAllOutsourcedService
{
  public static IResult Get(SqlConnection connectionString, int? pageNumber)
  {
    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    var outsourceds = GetAllOutsourcedRepository.Get(connectionString: connectionString, pageNumber: pageNumber);

    return Results.Ok(outsourceds);
  }
}