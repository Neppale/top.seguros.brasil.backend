static class GetAllOutsourcedRepository
{
  public static IEnumerable<Terceirizado> Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    return connectionString.Query<Terceirizado>("SELECT * from Terceirizados WHERE status = 'true' ORDER BY id_terceirizado OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
  }
}