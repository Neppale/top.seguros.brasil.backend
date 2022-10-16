static class GetAllIncidentRepository
{
    public static async Task<GetAllIncidentsDto> Get(SqlConnection connectionString, int? pageNumber, int? size)
    {
        var incidents = await connectionString.QueryAsync<Incident>("SELECT id_ocorrencia, Clientes.nome_completo AS nome, tipo, data, Ocorrencias.status FROM Ocorrencias LEFT JOIN Clientes ON Clientes.id_cliente = Ocorrencias.id_cliente ORDER BY id_ocorrencia DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
        var totalPages = await connectionString.QueryAsync<int>("SELECT CEILING(CAST(COUNT(*) AS FLOAT) / @Size) FROM Ocorrencias", new { Size = size });
        var response = new GetAllIncidentsDto(incidents, totalPages.First());
        return response;
    }
}