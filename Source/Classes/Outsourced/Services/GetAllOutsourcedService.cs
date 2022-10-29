public static class GetAllOutsourcedService
{
    public static async Task<IResult> Get(SqlConnection connectionString, int? pageNumber, int? size)
    {
        if (pageNumber == null) pageNumber = 1;
        if (size == null) size = 5;

        var outsourceds = await GetAllOutsourcedRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);
        var paginatedResponse = new paginatedResponse(data: outsourceds.outsourceds, totalPages: outsourceds.totalPages);

        return Results.Ok(paginatedResponse);
    }
}