using Dapper;
using Microsoft.Data.SqlClient;

static class InsertDocumentOcorrenciaService
{


  /** <summary> Esta função insere o documento de uma ocorrência no banco de dados. </summary>**/
  public static async Task<IResult> Insert(int id, HttpRequest request, string dbConnectionString)
  {
    //TODO: Fazer documento aceito ser arquivo em foramto png, jpg, jpeg, pdf. Por enquanto só aceita arquivo txt.
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    if (!request.HasFormContentType)
      return Results.BadRequest("Formato de requisição inválido.");

    var form = await request.ReadFormAsync();
    var formFile = form.Files["file"];

    if (formFile is null || formFile.Length == 0)
      return Results.BadRequest("Arquivo enviado não pode ser vazio.");

    // Verificando se ocorrência existe.
    bool ocorrenciaIsExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
    if (!ocorrenciaIsExistent) return Results.NotFound("Ocorrência não encontrada.");

    await using var stream = formFile.OpenReadStream();

    var reader = new StreamReader(stream);
    var file = await reader.ReadToEndAsync();
    try
    {
      connectionString.Query<Veiculo>("UPDATE Ocorrencias SET documento = CONVERT(varbinary(max), @File) WHERE id_ocorrencia = @Id", new { File = file, Id = id });
      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}