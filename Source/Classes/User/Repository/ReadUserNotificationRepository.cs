static class ReadUserNotificationRepository
{
  public static async void Read(int id, SqlConnection connectionString)
  {
    await connectionString.QueryAsync("UPDATE Notificacoes SET quantidade = 0 WHERE id_usuario = @id", new { id });
  }
}