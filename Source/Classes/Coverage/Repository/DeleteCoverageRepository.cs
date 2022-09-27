static class DeleteCoverageRepository
{
    public static async Task<int> Delete(int id, SqlConnection connectionString)
    {
        try
        {
            await connectionString.QueryAsync("UPDATE Coberturas SET status = 'false' WHERE id_cobertura = @Id", new { Id = id });
            return 1;
        }
        catch (SystemException)
        {
            return 0;
        }
    }
}