static class GetIncidentByClientService
{
    public static async Task<IResult> Get(int id_cliente, SqlConnection connectionString, int? pageNumber, int? size)
    {
        if (pageNumber == null) pageNumber = 1;
        if (size == null) size = 5;

        var incidents = await GetIncidentByClientRepository.Get(id: id_cliente, connectionString: connectionString, pageNumber: pageNumber, size: size);
        foreach (var incident in incidents.incidents) incident.data = Regex.Replace(incident.data, @"(\d{2})/(\d{2})/(\d{4})", "$2/$1/$3");
        var paginatedResponse = new paginatedResponse(data: incidents.incidents, totalPages: incidents.totalPages);

        return Results.Ok(paginatedResponse);
    }
}