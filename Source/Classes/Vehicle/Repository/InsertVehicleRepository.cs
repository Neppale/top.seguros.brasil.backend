static class InsertVehicleRepository
{
    public static async Task<Veiculo?> Insert(Veiculo vehicle, SqlConnection connectionString)
    {
        try
        {
            await connectionString.QueryAsync<Veiculo>("INSERT INTO Veiculos (marca, modelo, ano, uso, placa, renavam, sinistrado, id_cliente) VALUES (@Marca, @Modelo, @Ano, @Uso, @Placa, @Renavam, @Sinistrado, @IdCliente)", new { Marca = vehicle.marca, Modelo = vehicle.modelo, Ano = vehicle.ano, Uso = vehicle.uso, Placa = vehicle.placa, Renavam = vehicle.renavam, Sinistrado = vehicle.sinistrado, IdCliente = vehicle.id_cliente });

            var createdVehicle = await connectionString.QueryFirstOrDefaultAsync<Veiculo>("SELECT * FROM Veiculos WHERE renavam = @Renavam", new { Renavam = vehicle.renavam });
            return createdVehicle;
        }
        catch (SystemException)
        {
            return null;
        }
    }
}