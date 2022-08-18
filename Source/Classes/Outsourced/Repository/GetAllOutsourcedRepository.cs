static class GetAllOutsourcedRepository
{
  public static IEnumerable<Terceirizado> Get(SqlConnection connectionString, int? pageNumber)
  {
    if (pageNumber == null) pageNumber = 1;

    var outsourceds = connectionString.Query<Terceirizado>("SELECT * from Terceirizados WHERE status = 'true' ORDER BY id_terceirizado OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });

    return outsourceds;
  }
}