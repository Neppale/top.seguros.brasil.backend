static class InsertCoverageRepository
{
  public static GetOneCoverageDto? Insert(Cobertura cobertura, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("INSERT INTO Coberturas (nome, descricao, valor, taxa_indenizacao) VALUES (@Nome, @Descricao, @Valor, @TaxaIndenizacao)", new { Nome = cobertura.nome, Descricao = cobertura.descricao, Valor = cobertura.valor, TaxaIndenizacao = cobertura.taxa_indenizacao });

      var createdCoverage = connectionString.QueryFirstOrDefault<GetOneCoverageDto>("SELECT * FROM Coberturas WHERE nome = @Nome AND status = 'true'", new { Nome = cobertura.nome });
      return createdCoverage;
    }
    catch (SystemException)
    {
      return null;
    }
  }
}