static class GetAllIncidentRepository
{
  public static async Task<IEnumerable<GetAllIncidentsDto>> Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    return await connectionString.QueryAsync<GetAllIncidentsDto>("SELECT id_ocorrencia, Clientes.nome_completo AS nome, tipo, data, Ocorrencias.status FROM Ocorrencias LEFT JOIN Clientes ON Clientes.id_cliente = Ocorrencias.id_cliente ORDER BY id_ocorrencia DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
  }
}