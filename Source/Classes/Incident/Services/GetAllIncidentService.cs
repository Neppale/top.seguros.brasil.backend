static class GetAllIncidentService
{
    /** <summary> Esta função retorna todas as ocorrências no banco de dados. </summary>**/
    public static async Task<IResult> Get(SqlConnection connectionString, int? pageNumber, int? size)
    {
        if (pageNumber == null) pageNumber = 1;
        if (size == null) size = 5;

        var results = await GetAllIncidentRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);

        return Results.Ok(results);
    }
}