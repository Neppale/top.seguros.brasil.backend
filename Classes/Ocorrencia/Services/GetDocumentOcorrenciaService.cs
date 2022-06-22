using Dapper;
using Microsoft.Data.SqlClient;

static class GetDocumentOcorrenciaService
{
  /** <summary> Esta função retorna o documento de ocorrência específica no banco de dados. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      var data = connectionString.QueryFirstOrDefault<string>("SELECT documento from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
      if (data == null) return Results.NotFound("Ocorrência não encontrada, ou ocorrência não possui documento.");

      string fileName = DocumentConverter.Decode(data);
      return Results.File(fileName, contentType: "image/png");

    }
    catch (SystemException)
    {

      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}