static class InsertVehicleRepository
{
  public static int Insert(Veiculo veiculo, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query<Veiculo>("INSERT INTO Veiculos (marca, modelo, ano, uso, placa, renavam, sinistrado, id_cliente) VALUES (@Marca, @Modelo, @Ano, @Uso, @Placa, @Renavam, @Sinistrado, @IdCliente)", new { Marca = veiculo.marca, Modelo = veiculo.modelo, Ano = veiculo.ano, Uso = veiculo.uso, Placa = veiculo.placa, Renavam = veiculo.renavam, Sinistrado = veiculo.sinistrado, IdCliente = veiculo.id_cliente });

      // Retornando o id do veículo criado.
      int createdVeiculoId = connectionString.QueryFirstOrDefault<int>("SELECT id_veiculo FROM Veiculos WHERE renavam = @Renavam", new { Renavam = veiculo.renavam });

      return createdVeiculoId;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}