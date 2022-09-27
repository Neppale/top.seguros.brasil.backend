static class GetAllOutsourcedRepository
{
    public static async Task<IEnumerable<Terceirizado>> Get(SqlConnection connectionString, int? pageNumber, int? size)
    {
        return await connectionString.QueryAsync<Terceirizado>("SELECT * from Terceirizados WHERE status = 'true' ORDER BY id_terceirizado DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
    }
}