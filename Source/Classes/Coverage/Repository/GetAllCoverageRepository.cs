static class GetAllCoverageRepository
{
    public static async Task<IEnumerable<Cobertura>> Get(SqlConnection connectionString, int? pageNumber, int? size, string? search)
    {
        if (search != null) return await connectionString.QueryAsync<Cobertura>("SELECT * FROM Coberturas WHERE nome LIKE @Search AND status = 'true' ORDER BY id_cobertura DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size, Search = $"%{search}%" });
        else return await connectionString.QueryAsync<Cobertura>("SELECT * FROM Coberturas WHERE status = 'true' ORDER BY id_cobertura DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });

    }
}