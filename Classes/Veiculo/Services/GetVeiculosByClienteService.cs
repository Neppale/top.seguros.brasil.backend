static class GetVeiculosByCliente
{
  /** <summary>Esta função retorna todos os veículos do cliente.</summary> **/
  public static IResult Get(int id_cliente, int? pageNumber, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    var data = connectionString.Query("SELECT id_veiculo, marca, modelo, ano, uso, placa from Veiculos WHERE id_cliente = @Id ORDER BY id_veiculo OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { @Id = id_cliente, @PageNumber = (pageNumber - 1) * 5 });
    if (data.Count() == 0) return Results.NotFound("Nenhum veículo encontrado para o cliente, ou cliente não existe.");



    // Removendo caracteres especiais da exibição do modelo dos veículos da lista.
    foreach (var item in data)
    {
      item.modelo = VehicleModelUnformatter.Unformat(item.modelo);
    }

    return Results.Ok(data);

    // Exemplo de retorno, adaptada para o Management Stage: 
    // [{ 
    //   "id_veiculo": 1, 
    //   "marca": "Fiat", 
    //   "modelo": "Uno", 
    //   "ano": "2020 Gasolina", 
    //   "uso": "Trabalho", 
    //   "placa": "ABC-1234" 
    //   }]
  }
}