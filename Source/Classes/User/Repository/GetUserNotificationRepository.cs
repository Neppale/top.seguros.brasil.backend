static class GetUserNotificationRepository
{
    public static async Task<int> Get(int id, SqlConnection connectionString)
    {
        var amount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT quantidade FROM Notificacoes WHERE id_usuario = @id", new { id });
        return amount;
    }
}