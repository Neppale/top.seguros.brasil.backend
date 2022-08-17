static class GetIncidentDocumentRepository
{
  public static dynamic Get(int id, SqlConnection connectionString)
  {
    var document = connectionString.QueryFirstOrDefault("SELECT documento, tipoDocumento from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
    return document;
  }
}