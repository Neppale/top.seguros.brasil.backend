static class GetAllPolicyRepository
{
  public static IEnumerable<GetOnePolicyDto> Get(SqlConnection connectionString, int? pageNumber)
  {
    return connectionString.Query<GetOnePolicyDto>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE status != 'Rejeitada' ORDER BY id_apolice OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });
  }
}