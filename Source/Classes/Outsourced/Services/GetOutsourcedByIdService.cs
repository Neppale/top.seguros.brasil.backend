public static class GetOutsourcedByIdService
{
    /** <summary> Esta função retorna um terceirizado específico no banco de dados. </summary>**/
    public static async Task<IResult> Get(int id, SqlConnection connectionString)
    {
        var outsourced = await GetOutsourcedByIdRepository.Get(id: id, connectionString: connectionString);
        if (outsourced == null) return Results.NotFound(new { message = "Terceirizado não encontrado." });

        return Results.Ok(outsourced);
    }
}
