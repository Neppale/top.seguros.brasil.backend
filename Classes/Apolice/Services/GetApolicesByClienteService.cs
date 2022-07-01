static class GetApolicesByClienteService
{
  public static IResult Get(int id_cliente, int? pageNumber, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    var data = connectionString.Query("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE id_cliente = @Id ORDER BY id_apolice OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { Id = id_cliente, PageNumber = (pageNumber - 1) * 5 });
    if (data.Count() == 0) return Results.NotFound("Nenhuma apólice encontrada para o cliente, ou cliente não existe.");

    return Results.Ok(data);
  }
}