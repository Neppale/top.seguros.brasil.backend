static class GetIncidentDocumentRepository
{
  public static DocumentDto Get(int id, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<DocumentDto>("SELECT documento, tipoDocumento from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
  }
}