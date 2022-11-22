static class GetIncidentByOutsourcedRepository
{
  /** <summary> Esta função retorna uma lista de ocorrências de um terceirizado. </summary>**/
  public static async Task<IEnumerable<GetIncidentByOutsourcedDto>> Get(int id, SqlConnection connectionString, int pageNumber, int size)
  {
    var incidents = await connectionString.QueryAsync<GetIncidentByOutsourcedDto>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_terceirizado = @Id ORDER BY id_ocorrencia DESC", new { Id = id });

    foreach (var incident in incidents) incident.data = SqlDateConverter.ConvertToShow(incident.data);
    return incidents;
  }
}