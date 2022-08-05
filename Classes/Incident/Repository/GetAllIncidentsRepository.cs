static class GetAllIncidentRepository
{
  public static IEnumerable<Ocorrencia> Get(SqlConnection connectionString, int? pageNumber)
  {
    var incidents = connectionString.Query<Ocorrencia>("SELECT id_ocorrencia, Clientes.nome_completo AS nome, tipo, data, Ocorrencias.status FROM Ocorrencias LEFT JOIN Clientes ON Clientes.id_cliente = Ocorrencias.id_cliente ORDER BY id_ocorrencia OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });

    return incidents;
  }
}