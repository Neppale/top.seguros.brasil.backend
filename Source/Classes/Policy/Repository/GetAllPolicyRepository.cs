static class GetAllPolicyRepository
{
  public static async Task<IEnumerable<GetPolicyDto>> Get(SqlConnection connectionString, int? pageNumber, int? size)
  {
    return await connectionString.QueryAsync<GetPolicyDto>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE status != 'Rejeitada' ORDER BY id_apolice DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
  }
}