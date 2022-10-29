static class GetAllCoverageRepository
{
    public static async Task<PaginatedCoverages> Get(SqlConnection connectionString, int? pageNumber, int? size, string? search)
    {
        if (size == null) size = 5;

        Cobertura[] coverages;
        var totalPages = 0;

        if (search != null)
        {
            coverages = (await connectionString.QueryAsync<Cobertura>("SELECT * FROM Coberturas WHERE nome LIKE @Search AND status = 'true' ORDER BY id_cobertura DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size, Search = $"%{search}%" })).ToArray();
            var coverageCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Coberturas WHERE nome LIKE @Search AND status = 'true'", new { Search = $"%{search}%" });
            totalPages = (int)Math.Ceiling((double)coverageCount / (double)size);
        }
        else
        {
            coverages = (await connectionString.QueryAsync<Cobertura>("SELECT * FROM Coberturas WHERE status = 'true' ORDER BY id_cobertura DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
            var coverageCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Coberturas WHERE status = 'true'");
            totalPages = (int)Math.Ceiling((double)coverageCount / (double)size);
        }

        return new PaginatedCoverages(coverages: coverages, totalPages: totalPages);
    }
}