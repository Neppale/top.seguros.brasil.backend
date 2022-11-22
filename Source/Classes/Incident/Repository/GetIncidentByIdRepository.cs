static class GetIncidentByIdRepository
{
  public static async Task<GetIncidentByIdDto> Get(int id, SqlConnection connectionString)
  {
    var incident = await connectionString.QueryFirstOrDefaultAsync<GetIncidentByIdDto>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });

    incident.data = SqlDateConverter.ConvertToShow(incident.data);
    return incident;
  }
}