static class GetIncidentByOutsourcedRepository
{
  /** <summary> Esta função retorna uma lista de ocorrências de um terceirizado. </summary>**/
  public static IEnumerable<GetIncidentByOutsourcedDto> Get(int id, SqlConnection connectionString, int pageNumber, int size)
  {
    return connectionString.Query<GetIncidentByOutsourcedDto>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });
  }
}