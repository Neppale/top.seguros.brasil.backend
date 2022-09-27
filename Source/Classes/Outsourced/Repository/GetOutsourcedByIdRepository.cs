static class GetOutsourcedByIdRepository
{
    public static async Task<Terceirizado> Get(int id, SqlConnection connectionString)
    {
        return await connectionString.QueryFirstOrDefaultAsync<Terceirizado>("SELECT * FROM Terceirizados WHERE id_terceirizado = @Id", new { Id = id });
    }
}