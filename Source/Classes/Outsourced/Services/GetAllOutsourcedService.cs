public static class GetAllOutsourcedService
{
  public static IResult Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    if (pageNumber == null) pageNumber = 1;
    if (size == null) size = 5;

    var outsourceds = GetAllOutsourcedRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);

    return Results.Ok(outsourceds);
  }
}