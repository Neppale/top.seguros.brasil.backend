static class ReadUserNotificationRepository
{
  public static async void Read(int id, SqlConnection connectionString)
  {
    await connectionString.QueryAsync("UPDATE Usuarios SET notificacoes = 0 WHERE id_usuario = @id", new { id });
  }
}