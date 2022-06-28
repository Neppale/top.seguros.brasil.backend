static class GetDocumentOcorrenciaService
{
  /** <summary> Esta função retorna o documento de ocorrência específica no banco de dados. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      var data = connectionString.QueryFirstOrDefault("SELECT documento, tipoDocumento from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
      if (data.documento == null) return Results.NotFound("Ocorrência não encontrada, ou ocorrência não possui documento.");

      string fileName = DocumentConverter.Decode(data.documento, data.tipoDocumento);
      return Results.File(fileName, contentType: data.tipoDocumento);

    }
    catch (SystemException)
    {

      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}