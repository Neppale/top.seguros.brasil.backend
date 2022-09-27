public static class DeleteCoverageService
{
    /** <summary> Esta função desativa uma cobertura no banco de dados. </summary>**/
    public static async Task<IResult> Delete(int id, SqlConnection connectionString)
    {
        var coverage = await GetCoverageByIdRepository.Get(id: id, connectionString: connectionString);
        if (coverage == null) return Results.NotFound(new { message = "Cobertura não encontrada." });

        var result = await DeleteCoverageRepository.Delete(id: id, connectionString: connectionString);
        if (result == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

        return Results.NoContent();
    }
}