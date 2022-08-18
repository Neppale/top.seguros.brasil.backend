static class GetOneIncidentRepository
{
  public static GetOneIncidentDto Get(int id, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<GetOneIncidentDto>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
  }
}