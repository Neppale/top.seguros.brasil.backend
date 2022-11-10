static class GetAllPolicyService
{
    /** <summary> Esta função retorna as apólices no banco de dados. </summary>**/
    public static async Task<IResult> Get(SqlConnection connectionString, int? pageNumber, int? size)
    {
        if (pageNumber == null) pageNumber = 1;
        if (size == null) size = 5;

        var result = await GetAllPolicyRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);
        foreach (var policy in result.policies)
        {
            policy.data_inicio = Regex.Replace(policy.data_inicio, @"(\d{2})/(\d{2})/(\d{4})", "$2/$1/$3");
            policy.data_fim = Regex.Replace(policy.data_fim, @"(\d{2})/(\d{2})/(\d{4})", "$2/$1/$3");
        }

        var paginatedResponse = new paginatedResponse(data: result.policies, totalPages: result.totalPages);

        return Results.Ok(paginatedResponse);
    }
}