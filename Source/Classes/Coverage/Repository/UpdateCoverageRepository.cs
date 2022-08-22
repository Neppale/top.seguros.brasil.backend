static class UpdateCoverageRepository
{
  public static GetCoverageByIdDto? Update(int id, Cobertura cobertura, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Coberturas SET nome = @Nome, descricao = @Descricao, valor = @Valor, taxa_indenizacao = @TaxaIndenizacao WHERE id_cobertura = @Id", new { Nome = cobertura.nome, Descricao = cobertura.descricao, Valor = cobertura.valor, Id = id, TaxaIndenizacao = cobertura.taxa_indenizacao });

      var updatedCoverage = connectionString.QueryFirstOrDefault<GetCoverageByIdDto>("SELECT * FROM Coberturas WHERE id_cobertura = @Id AND status = 'true'", new { Id = id });
      return updatedCoverage;
    }
    catch (SystemException)
    {
      return null;
    }
  }
}