using Dapper;
using Microsoft.Data.SqlClient;

static class InsertDocumentOcorrenciaService
{
  public static async Task<IResult> Insert(int id, HttpRequest request, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se formato de requisição é válido.
    if (!request.HasFormContentType)
      return Results.BadRequest("Formato de requisição inválido.");

    // Verificando se arquivo é nulo.
    var formRequest = await request.ReadFormAsync();
    var formFile = formRequest.Files.GetFile("file");
    if (formFile == null || formFile.Length == 0) return Results.BadRequest("Arquivo enviado não pode ser vazio.");
    if (formFile.ContentType != "image/png") return Results.BadRequest("Formato de arquivo inválido. Formatos aceitos: PNG.");

    // Lendo conteúdo do arquivo e convertendo para base64.
    Stream fileReader = formFile.OpenReadStream();
    string fileBase64 = DocumentConverter.Encode(fileReader);

    // Verificando se ocorrência existe.
    bool ocorrenciaExists = connectionString.QueryFirstOrDefault<bool>("SELECT id_ocorrencia FROM Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
    if (!ocorrenciaExists) return Results.NotFound("Ocorrência não encontrada.");

    try
    {
      connectionString.Query("UPDATE Ocorrencias SET documento = @File WHERE id_ocorrencia = @Id", new { File = fileBase64, Id = id });
      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}