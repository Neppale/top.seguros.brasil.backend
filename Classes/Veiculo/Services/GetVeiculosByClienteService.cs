static class GetVeiculosByCliente
{
  public static IResult Get(int id_cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Veiculo>("SELECT * from Veiculos WHERE id_cliente = @id_cliente", new { id_cliente });
    if (data.Count() == 0) return Results.NotFound("Nenhum veículo encontrado para o cliente, ou cliente não existe.");

    return Results.Ok(data);
  }
}