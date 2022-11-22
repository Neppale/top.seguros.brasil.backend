static class GetAllIncidentRepository
{
  public static async Task<PaginatedIncidents> Get(SqlConnection connectionString, int? pageNumber, int? size, string? search)
  {
    if (size == null) size = 5;

    GetAllIncidentsDto[] incidents;
    var totalPages = 0;

    if (search != null)
    {
      incidents = (await connectionString.QueryAsync<GetAllIncidentsDto>("SELECT id_ocorrencia, Clientes.nome_completo AS nome, tipo, data, Ocorrencias.status FROM Ocorrencias LEFT JOIN Clientes ON Clientes.id_cliente = Ocorrencias.id_cliente WHERE Clientes.nome_completo LIKE @Search ORDER BY id_ocorrencia DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size, Search = $"%{search}%" })).ToArray();
      var incidentCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Ocorrencias LEFT JOIN Clientes ON Clientes.id_cliente = Ocorrencias.id_cliente WHERE Clientes.nome_completo LIKE @Search", new { Search = $"%{search}%" });
      totalPages = (int)Math.Ceiling((double)incidentCount / (double)size);
    }
    else
    {
      incidents = (await connectionString.QueryAsync<GetAllIncidentsDto>("SELECT id_ocorrencia, Clientes.nome_completo AS nome, tipo, data, Ocorrencias.status FROM Ocorrencias LEFT JOIN Clientes ON Clientes.id_cliente = Ocorrencias.id_cliente ORDER BY id_ocorrencia DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
      var incidentCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Ocorrencias");
      totalPages = (int)Math.Ceiling((double)incidentCount / (double)size);
    }

    foreach (var incident in incidents) incident.data = SqlDateConverter.ConvertToShow(incident.data);
    return new PaginatedIncidents(incidents: incidents, totalPages: totalPages);
  }
}