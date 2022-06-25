static class GetApolicesByClienteService
{
  public static IResult Get(int id_cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Apolice>("SELECT * from Apolices WHERE id_cliente = @id_cliente", new { id_cliente = id_cliente });
    if (data.Count() == 0) return Results.NotFound("Nenhuma apólice encontrada para o cliente, ou cliente não existe.");

    return Results.Ok(data);
  }
}