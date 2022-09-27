static class GetUserByIdService
{
    /** <summary> Esta função retorna um usuário específico no banco de dados. </summary>**/
    public static async Task<IResult> Get(int id, SqlConnection connectionString)
    {
        var user = await GetUserByIdRepository.Get(id: id, connectionString: connectionString);
        if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });

        return Results.Ok(user);
    }
}

