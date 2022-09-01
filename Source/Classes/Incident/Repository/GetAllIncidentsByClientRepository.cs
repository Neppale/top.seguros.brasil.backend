static class GetAllIncidentByClientRepository
{
  public static IEnumerable<GetIncidentByIdDto> Get(int id, SqlConnection connectionString, int? pageNumber, int? size)
  {
    return connectionString.Query<GetIncidentByIdDto>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_cliente = @Id ORDER BY id_ocorrencia OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
  }
}