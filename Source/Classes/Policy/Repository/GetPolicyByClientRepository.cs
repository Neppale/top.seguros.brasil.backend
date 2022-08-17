static class GetPolicyByClientRepository
{
  public static IEnumerable<dynamic> Get(int id, SqlConnection connectionString, int? pageNumber)
  {
    var policies = connectionString.Query<dynamic>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE id_cliente = @Id ORDER BY id_apolice OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { Id = id, PageNumber = (pageNumber - 1) * 5 });

    return policies;
  }
}