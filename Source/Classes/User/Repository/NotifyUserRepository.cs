static class NotifyUserRepository
{
  /** <summary> Esta função notifica o usuário sobre a criação de uma nova apólice. </summary>**/
  public static async Task Notify(int id, SqlConnection connectionString)
  {
    var userNotification = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT notificacoes FROM Usuarios WHERE id = @id", new { id = id });
    await connectionString.QueryAsync("UPDATE Usuarios SET notificacoes = @notifications WHERE id = @id", new { notifications = userNotification + 1, id = id });
  }
}