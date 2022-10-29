static class GetUserNotificationService
{
    public static async Task<IResult> Get(int id, SqlConnection connectionString)
    {
        var user = await GetUserByIdRepository.Get(id: id, connectionString: connectionString);
        if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });


        var amount = await GetUserNotificationRepository.Get(id: id, connectionString: connectionString);
        return Results.Ok(amount);
    }
}