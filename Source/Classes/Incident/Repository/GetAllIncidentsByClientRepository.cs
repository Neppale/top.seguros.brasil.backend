static class GetAllIncidentByClientRepository
{
  public static IResult Get(int id, SqlConnection connectionString, int? pageNumber)
  {
    var results = connectionString.Query("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_cliente = @Id", new { Id = id });

    return Results.Ok(results);
  }
}