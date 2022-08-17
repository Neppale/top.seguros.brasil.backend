static class InsertIncidentDocumentService
{
  private static readonly string[] validExtensions = { "image/png", "image/jpg", "image/jpeg" };
  public static async Task<IResult> Insert(int id, HttpRequest request, SqlConnection connectionString)
  {
    if (!request.HasFormContentType) return Results.BadRequest("Formato de requisição inválido.");

    var formRequest = await request.ReadFormAsync();
    var formFile = formRequest.Files.GetFile("file");
    if (formFile == null || formFile.Length == 0) return Results.BadRequest("Arquivo enviado não pode ser vazio.");
    if (!validExtensions.Contains(formFile.ContentType)) return Results.BadRequest("Formato de arquivo inválido. Formatos aceitos: " + string.Join(", ", validExtensions));

    Stream fileReader = formFile.OpenReadStream();
    string fileBase64 = DocumentConverter.Encode(fileReader);
    
    var incident = GetOneIncidentRepository.Get(id, connectionString);
    if (incident == null) return Results.NotFound("Ocorrência não encontrada.");

    var result = InsertIncidentDocumentRepository.Insert(id: id, fileType: formFile.ContentType, fileBase64: fileBase64, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/ocorrencia/{id}/documento", "Documento inserido com sucesso.");
  }
}