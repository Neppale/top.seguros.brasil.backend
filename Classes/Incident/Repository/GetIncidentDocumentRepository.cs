static class GetIncidentDocumentRepository
{
  public static dynamic Get(int id, SqlConnection connectionString)
  {
    try
    {
      var document = connectionString.QueryFirstOrDefault("SELECT documento, tipoDocumento from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });

      return document;
    }
    catch (SystemException)
    {
      return null;
    }
  }
}