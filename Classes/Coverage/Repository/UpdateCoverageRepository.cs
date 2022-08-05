static class UpdateCoverageRepository
{
  public static int Update(int id, Cobertura cobertura, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Coberturas SET nome = @Nome, descricao = @Descricao, valor = @Valor, taxa_indenizacao = @TaxaIndenizacao WHERE id_cobertura = @Id", new { Nome = cobertura.nome, Descricao = cobertura.descricao, Valor = cobertura.valor, Id = id, TaxaIndenizacao = cobertura.taxa_indenizacao });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}