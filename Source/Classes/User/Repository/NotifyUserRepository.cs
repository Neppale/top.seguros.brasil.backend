static class NotifyUserRepository
{
    /** <summary> Esta função notifica o usuário sobre a criação de uma nova apólice. </summary>**/
    public static async Task Notify(int id, SqlConnection connectionString)
    {
        var userNotification = await connectionString.QueryFirstOrDefaultAsync("SELECT * FROM Notificacoes WHERE id_usuario = @id", new { id });
        if (userNotification == null) await connectionString.QueryAsync("INSERT INTO Notificacoes (id_usuario, quantidade) VALUES (@id, @amount)", new { id, amount = 1 });
        else await connectionString.QueryAsync("UPDATE Notificacoes SET quantidade = quantidade + 1 WHERE id_usuario = @id", new { id });
    }
}