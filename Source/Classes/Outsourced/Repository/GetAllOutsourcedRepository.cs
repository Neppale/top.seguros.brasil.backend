static class GetAllOutsourcedRepository
{
  public static async Task<PaginatedOutsourced> Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    Terceirizado[] outsourceds;
    outsourceds = (await connectionString.QueryAsync<Terceirizado>("SELECT * from Terceirizados WHERE status = 'true' ORDER BY id_terceirizado DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
    var totalPages = await connectionString.QueryFirstAsync<int>("SELECT COUNT(*) FROM Terceirizados WHERE status = 'true'");

    var paginatedOutsourced = new PaginatedOutsourced(outsourceds: outsourceds, totalPages: totalPages);

    return paginatedOutsourced;
  }
}