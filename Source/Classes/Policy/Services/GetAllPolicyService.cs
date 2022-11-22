static class GetAllPolicyService
{
    /** <summary> Esta função retorna as apólices no banco de dados. </summary>**/
    public static async Task<IResult> Get(SqlConnection connectionString, int? pageNumber, int? size)
    {
        if (pageNumber == null) pageNumber = 1;
        if (size == null) size = 5;

        var result = await GetAllPolicyRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);
        var paginatedResponse = new paginatedResponse(data: result.policies, totalPages: result.totalPages);

        return Results.Ok(paginatedResponse);
    }
}