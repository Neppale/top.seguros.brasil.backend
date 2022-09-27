static class GetPolicyDocumentService
{
    public static async Task<IResult> Get(int id, SqlConnection connectionString)
    {
        var policy = await GetPolicyByIdRepository.Get(id: id, connectionString: connectionString);
        if (policy == null) return Results.NotFound(new { message = "Apólice não encontrada." });

        try
        {
            string document = await GetPolicyDocumentRepository.Get(id: id, connectionString: connectionString);
            string filePath = DocumentConverter.Decode(document, "application/pdf");

            return Results.File(path: filePath, contentType: "application/pdf");

        }
        catch (SystemException)
        {
            return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde. " });
        }
    }
}