static class GetPolicyByClientRepository
{
  public static async Task<IEnumerable<GetPolicyDto>> Get(int id, SqlConnection connectionString, int? pageNumber, int? size)
  {
    return await connectionString.QueryAsync<GetPolicyDto>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE id_cliente = @Id ORDER BY id_apolice DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { Id = id, PageNumber = (pageNumber - 1) * size, Size = size });
  }
}