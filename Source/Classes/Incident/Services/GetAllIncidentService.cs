static class GetAllIncidentService
{
  /** <summary> Esta função retorna todas as ocorrências no banco de dados. </summary>**/
  public static async Task<IResult> Get(SqlConnection connectionString, int? pageNumber, int? size, string? search)
  {
    if (pageNumber == null) pageNumber = 1;
    if (size == null) size = 5;

    var incidents = await GetAllIncidentRepository.Get(connectionString: connectionString, pageNumber: pageNumber, size: size, search: search);
    var paginatedResponse = new paginatedResponse(data: incidents.incidents, totalPages: incidents.totalPages);

    return Results.Ok(paginatedResponse);
  }
}