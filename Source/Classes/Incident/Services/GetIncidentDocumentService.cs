static class GetIncidentDocumentService
{
  /** <summary> Esta função retorna o documento de ocorrência específica no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var document = GetIncidentDocumentRepository.Get(id: id, connectionString: connectionString);
    if (document == null) return Results.NotFound(new { message = "Documento não encontrado." });

    string fileName = DocumentConverter.Decode(document.documento, document.tipoDocumento);

    return Results.File(fileName, contentType: document.tipoDocumento);
  }
}