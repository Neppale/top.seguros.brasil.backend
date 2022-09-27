static class GetIncidentDocumentRepository
{
    public static Task<DocumentDto> Get(int id, SqlConnection connectionString)
    {
        return connectionString.QueryFirstOrDefaultAsync<DocumentDto>("SELECT documento, tipoDocumento from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
    }
}