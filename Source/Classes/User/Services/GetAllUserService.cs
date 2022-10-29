static class GetAllUserService
{
    /** <summary> Esta função retorna todos os usuários no banco de dados. </summary>**/
    public static async Task<IResult> Get(SqlConnection connectionString, int? pageNumber, int? size, string? search)
    {
        if (pageNumber == null) pageNumber = 1;
        if (size == null) size = 5;

        var users = await GetAllUserRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size, search: search);
        var paginatedResponse = new paginatedResponse(data: users.users, totalPages: users.totalPages);

        return Results.Ok(paginatedResponse);
    }
}