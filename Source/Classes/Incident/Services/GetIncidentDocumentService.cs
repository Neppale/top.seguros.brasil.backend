static class GetIncidentDocumentService
{
    /** <summary> Esta função retorna o documento de ocorrência específica no banco de dados. </summary>**/
    public static async Task<IResult> Get(int id, SqlConnection connectionString)
    {
        var incident = await GetIncidentByIdRepository.Get(id: id, connectionString: connectionString);
        if (incident == null) return Results.NotFound(new { message = "Ocorrência não encontrada." });

        var document = await GetIncidentDocumentRepository.Get(id: id, connectionString: connectionString);
        if (document.documento == null || document.tipoDocumento == null) return Results.NotFound(new { message = "Documento não encontrado." });

        var documentStream = DocumentConverter.Decode(document.documento, document.tipoDocumento);

        string fileType = document.tipoDocumento.Split('/')[1];
        string fileName = $"Ocorrencia-{Guid.NewGuid()}.{fileType}";

        return Results.File(documentStream, contentType: document.tipoDocumento, fileDownloadName: fileName);
    }
}