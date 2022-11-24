static class GetIncidentDocumentRepository
{
    public static async Task<DocumentDto> Get(int id, SqlConnection connectionString)
    {
        var document = await connectionString.QueryFirstOrDefaultAsync<DocumentDto>("SELECT documento, tipoDocumento from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
        // if document.tipoDocumento == "any_document_type" it means that the document does not exist. change document.tipoDocumento and document.documento to null
        if (document.tipoDocumento == "any_document_type")
        {
            document.tipoDocumento = null!;
            document.documento = null!;
        }
        return document;
    }
}