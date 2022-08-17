static class GetOneIncidentRepository
{
  public static dynamic Get(int id, SqlConnection connectionString)
  {
    var incident = connectionString.QueryFirstOrDefault<dynamic>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
    return incident;
  }
}