static class GetClientByIdService
{
    /** <summary> Esta função retorna um cliente em específico no banco de dados. </summary>**/
    public static async Task<IResult> Get(int id, SqlConnection connectionString)
    {
        var client = await GetClientByIdRepository.Get(id: id, connectionString: connectionString);
        if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

        return Results.Ok(client);
    }
}