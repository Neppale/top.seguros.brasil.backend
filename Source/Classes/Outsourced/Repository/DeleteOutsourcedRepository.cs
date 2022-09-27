
static class DeleteOutsourcedRepository
{
    public static async Task<int> Delete(int id, SqlConnection connectionString)
    {
        try
        {
            await connectionString.QueryAsync("UPDATE Terceirizados SET status = 'false' WHERE id_terceirizado = @Id", new { Id = id });
            return 1;
        }
        catch (SystemException)
        {
            return 0;
        }
    }
}