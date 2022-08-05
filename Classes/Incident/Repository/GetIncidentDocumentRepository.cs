static class GetIncidentDocumentRepository
{
  public static dynamic Get(int id, SqlConnection connectionString)
  {
    try
    {
      var document = connectionString.QueryFirstOrDefault("SELECT documento, tipoDocumento from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
      if (document.documento == null) return Results.NotFound("Ocorrência não encontrada, ou ocorrência não possui documento.");

      return document;
    }
    catch (SystemException)
    {
      return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");
    }
  }
}