static class GetUserNotificationRepository
{
    public static async Task<Notification> Get(int id, SqlConnection connectionString)
    {
        var amount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT notificacoes FROM Usuarios WHERE id_usuario = @id", new { id });
        return new Notification(amount);
    }
}