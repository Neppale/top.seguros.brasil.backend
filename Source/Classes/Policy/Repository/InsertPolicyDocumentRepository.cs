public static class InsertPolicyDocumentRepository
{
    public static async Task Insert(int id, string document, SqlConnection connectionString)
    {
        await connectionString.QueryAsync("UPDATE Apolices SET documento = @Documento WHERE id_apolice = @IdApolice", new { Documento = document, IdApolice = id });
    }
}