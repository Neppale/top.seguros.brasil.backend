static class GetVeiculosByCliente
{
  /** <summary>Esta função retorna todos os veículos do cliente.</summary> **/
  public static IResult Get(int id_cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Veiculo>("SELECT * from Veiculos WHERE id_cliente = @id_cliente", new { id_cliente });
    if (data.Count() == 0) return Results.NotFound("Nenhum veículo encontrado para o cliente, ou cliente não existe.");

    // Removendo caracteres especiais da exibição do modelo dos veículos da lista.
    foreach (var item in data)
    {
      item.modelo = item.modelo.Replace(@"\", "");
    }

    return Results.Ok(data);
  }
}