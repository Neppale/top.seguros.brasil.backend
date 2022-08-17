public static class GetAllOutsourcedService
{
  public static IResult Get(SqlConnection connectionString, int? pageNumber)
  {
    if (pageNumber == null) pageNumber = 1;

    var outsourceds = GetAllOutsourcedRepository.Get(connectionString: connectionString, pageNumber: pageNumber);

    return Results.Ok(outsourceds);
  }
}