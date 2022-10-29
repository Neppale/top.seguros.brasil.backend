static class GetIncidentByClientRepository
{
  public static async Task<PaginatedIncidentsByClient> Get(int id, SqlConnection connectionString, int? pageNumber, int? size)
  {
    if (size == null) size = 5;

    GetIncidentByIdDto[] incidents = new GetIncidentByIdDto[0];
    var totalPages = 0;

    incidents = (await connectionString.QueryAsync<GetIncidentByIdDto>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_cliente = @Id ORDER BY id_ocorrencia DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { Id = id, PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
    var incidentCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Ocorrencias WHERE id_cliente = @Id", new { Id = id });
    totalPages = (int)Math.Ceiling((double)incidentCount / (double)size);

    return new PaginatedIncidentsByClient(incidents: incidents, totalPages: totalPages);
  }
}