static class GetAllIncidentByClientRepository
{
  public static IEnumerable<GetIncidentByIdDto> Get(int id, SqlConnection connectionString, int? pageNumber)
  {
    return connectionString.Query<GetIncidentByIdDto>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_cliente = @Id", new { Id = id });
  }
}