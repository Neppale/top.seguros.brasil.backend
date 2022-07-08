public static class GetAllVehicleService
{
  /** <summary> Esta função retorna todos os veículos no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString, int? pageNumber)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    var data = connectionString.Query("SELECT id_veiculo, marca, modelo, Clientes.nome_completo AS dono, placa FROM Veiculos LEFT JOIN Clientes ON Clientes.id_cliente = Veiculos.id_cliente WHERE Veiculos.status = 'true' ORDER BY id_veiculo OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });

    // Removendo caracteres especiais da exibição do modelo dos veículos da lista.
    foreach (var item in data)
    {
      item.modelo = VehicleModelUnformatter.Unformat(item.modelo);
    }


    return Results.Ok(data);
  }
}