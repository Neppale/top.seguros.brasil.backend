static class GetIncidentByIdRepository
{
  public static GetIncidentByIdDto Get(int id, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<GetIncidentByIdDto>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
  }
}