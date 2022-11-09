static class GetAllIncidentService
{
    /** <summary> Esta função retorna todas as ocorrências no banco de dados. </summary>**/
    public static async Task<IResult> Get(SqlConnection connectionString, int? pageNumber, int? size, string? search)
    {
        if (pageNumber == null) pageNumber = 1;
        if (size == null) size = 5;

        var incidents = await GetAllIncidentRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size, search: search);
        // for each incident, change the date format in property data from MM/dd/yyyy to dd/MM/yyyy using regex
        foreach (var incident in incidents.incidents) incident.data = Regex.Replace(incident.data, @"(\d{2})/(\d{2})/(\d{4})", "$2/$1/$3");
        var paginatedResponse = new paginatedResponse(data: incidents.incidents, totalPages: incidents.totalPages);

        return Results.Ok(paginatedResponse);
    }
}