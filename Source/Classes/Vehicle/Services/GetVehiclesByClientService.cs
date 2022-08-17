static class GetVehiclesByClient
{
  /** <summary>Esta função retorna todos os veículos do cliente.</summary> **/
  public static IResult Get(int id_cliente, int? pageNumber, SqlConnection connectionString)
  {
    if (pageNumber == null) pageNumber = 1;

    var client = GetOneClientRepository.Get(id: id_cliente, connectionString: connectionString);
    if (client == null) return Results.NotFound("Cliente não encontrado.");

    var results = GetVehiclesByClientRepository.Get(id: id_cliente, connectionString: connectionString, pageNumber: pageNumber);

    foreach (var item in results)
    {
      item.modelo = VehicleModelUnformatter.Unformat(item.modelo);
    }

    return Results.Ok(results);

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