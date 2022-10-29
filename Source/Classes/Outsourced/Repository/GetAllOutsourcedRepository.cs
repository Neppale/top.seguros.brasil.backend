static class GetAllOutsourcedRepository
{
  public static async Task<PaginatedOutsourced> Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    if (size == null) size = 5;

    Terceirizado[] outsourceds;
    outsourceds = (await connectionString.QueryAsync<Terceirizado>("SELECT * from Terceirizados WHERE status = 'true' ORDER BY id_terceirizado DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
    var outsourcedCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Terceirizados WHERE status = 'true'");
    var totalPages = (int)Math.Ceiling((double)outsourcedCount / (int)size);

    var paginatedOutsourced = new PaginatedOutsourced(outsourceds: outsourceds, totalPages: totalPages);

    return paginatedOutsourced;
  }
}