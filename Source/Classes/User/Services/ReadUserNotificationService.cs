static class ReadUserNotificationService
{
    public static async Task<IResult> Read(int id, SqlConnection connectionString)
    {
        var user = await GetUserByIdRepository.Get(id: id, connectionString: connectionString);
        if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });

        ReadUserNotificationRepository.Read(id: id, connectionString: connectionString);
        return Results.NoContent();
    }
}