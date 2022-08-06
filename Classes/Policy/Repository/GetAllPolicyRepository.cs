static class GetAllPolicyRepository
{
  public static IEnumerable<Apolice> Get(SqlConnection connectionString, int? pageNumber)
  {
    var policies = connectionString.Query<Apolice>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE status != 'Rejeitada' ORDER BY id_apolice OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });

    return policies;
  }
}