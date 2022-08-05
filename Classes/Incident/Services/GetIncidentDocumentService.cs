static class GetIncidentDocumentService
{
  /** <summary> Esta função retorna o documento de ocorrência específica no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var document = GetIncidentDocumentRepository.Get(id, connectionString);
    if (document == null) return Results.NotFound("Documento não encontrado.");

    string fileName = DocumentConverter.Decode(document.documento, document.tipoDocumento);

    return Results.File(fileName, contentType: document.tipoDocumento);
  }
}