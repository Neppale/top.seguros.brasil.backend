static class VehicleAlreadyExistsValidator
{
  public static async Task<bool> Validate(Veiculo vehicle, SqlConnection connectionString)
  {
    bool plateAlreadyExists = await connectionString.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS (SELECT placa FROM Veiculos WHERE placa = @Placa AND status = 'true') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Placa = vehicle.placa });
    if (plateAlreadyExists) return false;

    bool renavamAlreadyExists = await connectionString.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS (SELECT renavam FROM Veiculos WHERE renavam = @Renavam AND status = 'true') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Renavam = vehicle.renavam });
    if (renavamAlreadyExists) return false;

    return true;
  }

  public static async Task<bool> Validate(int id, Veiculo vehicle, SqlConnection connectionString)
  {
    bool plateAlreadyExists = await connectionString.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS (SELECT placa FROM Veiculos WHERE placa = @Placa AND status = 'true' AND id_veiculo != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Placa = vehicle.placa, Id = id });
    if (plateAlreadyExists) return false;

    bool renavamAlreadyExists = await connectionString.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS (SELECT renavam FROM Veiculos WHERE renavam = @Renavam AND status = 'true' AND id_veiculo != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Renavam = vehicle.renavam, Id = id });
    if (renavamAlreadyExists) return false;

    return true;
  }
}