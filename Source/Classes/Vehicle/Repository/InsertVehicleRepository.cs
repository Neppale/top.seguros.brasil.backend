static class InsertVehicleRepository
{
  public static Veiculo? Insert(Veiculo vehicle, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query<Veiculo>("INSERT INTO Veiculos (marca, modelo, ano, uso, placa, renavam, sinistrado, id_cliente) VALUES (@Marca, @Modelo, @Ano, @Uso, @Placa, @Renavam, @Sinistrado, @IdCliente)", new { Marca = vehicle.marca, Modelo = vehicle.modelo, Ano = vehicle.ano, Uso = vehicle.uso, Placa = vehicle.placa, Renavam = vehicle.renavam, Sinistrado = vehicle.sinistrado, IdCliente = vehicle.id_cliente });

      var createdVehicle = connectionString.QueryFirstOrDefault<Veiculo>("SELECT * FROM Veiculos WHERE renavam = @Renavam", new { Renavam = vehicle.renavam });
      return createdVehicle;
    }
    catch (SystemException)
    {
      return null;
    }
  }
}