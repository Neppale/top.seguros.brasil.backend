using Dapper;
using Microsoft.Data.SqlClient;
static class GetDocumentOcorrenciaService
{
  /** <summary> Esta função retorna o documento de ocorrência específica no banco de dados. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    //TODO: Retornar documento das ocorrências.
    try
    {
      var data = connectionString.QueryFirstOrDefault<string>("SELECT CAST(documento AS varchar(max)) from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
      if (data == null) return Results.NotFound("Ocorrência não encontrada, ou ocorrência não possui documento.");

      return Results.Ok(data);

    }
    catch (SystemException)
    {

      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}