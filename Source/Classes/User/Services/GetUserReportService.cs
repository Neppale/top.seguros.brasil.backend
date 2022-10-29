static class GetUserReportService
{
    public static async Task<IResult> Get(SqlConnection connectionString, int id)
    {
        var user = await GetUserByIdRepository.Get(id, connectionString);
        if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });

        var report = await GetUserReportRepository.Get(connectionString: connectionString, id: id);
        return Results.Ok(report);
    }
}