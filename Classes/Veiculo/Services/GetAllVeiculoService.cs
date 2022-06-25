public static class GetAllVeiculoService
{
  /** <summary> Esta função retorna todos os veículos no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Veiculo>("SELECT * from Veiculos WHERE status = 'true'");

    // Removendo caracteres especiais da exibição do modelo dos veículos da lista.
    foreach (var item in data)
    {
      item.modelo = item.modelo.Replace(@"\", "");
    }


    return Results.Ok(data);
  }
}