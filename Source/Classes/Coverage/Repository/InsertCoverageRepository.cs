static class InsertCoverageRepository
{
  public static int Insert(Cobertura cobertura, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("INSERT INTO Coberturas (nome, descricao, valor, taxa_indenizacao) VALUES (@Nome, @Descricao, @Valor, @TaxaIndenizacao)", new { Nome = cobertura.nome, Descricao = cobertura.descricao, Valor = cobertura.valor, TaxaIndenizacao = cobertura.taxa_indenizacao });

      // Pegando o ID da cobertura que acabou de ser inserida.
      int createdCoberturaId = connectionString.QueryFirstOrDefault<int>("SELECT id_cobertura FROM Coberturas WHERE nome = @Nome", new { Nome = cobertura.nome });

      return createdCoberturaId;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}