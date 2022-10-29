static class GetPolicyByUserService
{
    public static async Task<IResult> Get(int id_usuario, SqlConnection connectionString, int? pageNumber, int? size, string? search)
    {

        var user = await GetUserByIdRepository.Get(id: id_usuario, connectionString: connectionString);
        if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });

        if (pageNumber == null) pageNumber = 1;
        if (size == null) size = 5;

        var data = await GetPolicyByUserRepository.Get(id: id_usuario, connectionString: connectionString, pageNumber: pageNumber, size: size, search: search);
        var paginatedResponse = new paginatedResponse(data: data.policies, totalPages: data.totalPages);

        return Results.Ok(paginatedResponse);
    }
}
