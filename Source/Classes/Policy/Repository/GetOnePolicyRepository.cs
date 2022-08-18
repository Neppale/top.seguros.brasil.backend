static class GetOnePolicyRepository
{
  public static GetOnePolicyDto Get(int id, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<GetOnePolicyDto>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE id_apolice = @Id", new { Id = id });
  }
}