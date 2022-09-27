static class UpdateCoverageRepository
{
    public static async Task<GetCoverageByIdDto?> Update(int id, Cobertura cobertura, SqlConnection connectionString)
    {
        try
        {
            await connectionString.QueryAsync("UPDATE Coberturas SET nome = @Nome, descricao = @Descricao, valor = @Valor, taxa_indenizacao = @TaxaIndenizacao WHERE id_cobertura = @Id", new { Nome = cobertura.nome, Descricao = cobertura.descricao, Valor = cobertura.valor, Id = id, TaxaIndenizacao = cobertura.taxa_indenizacao });

            var updatedCoverage = await connectionString.QueryFirstOrDefaultAsync<GetCoverageByIdDto>("SELECT * FROM Coberturas WHERE id_cobertura = @Id AND status = 'true'", new { Id = id });
            return updatedCoverage;
        }
        catch (SystemException)
        {
            return null;
        }
    }
}