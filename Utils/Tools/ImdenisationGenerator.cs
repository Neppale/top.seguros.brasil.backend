static class IndemnisationGenerator
{
  /** <summary> Esta função gera um valor de indenização. </summary>**/
  public static async Task<decimal> Generate(int id_veiculo, string dbConnectionString)
  {
    SqlConnection connection = new SqlConnection(dbConnectionString);

    // Recuperar dados do veículo no banco.
    Veiculo veiculo = connection.QueryFirst<Veiculo>("SELECT * FROM Veiculos WHERE id_veiculo = @id", new { id = id_veiculo });

    decimal indemnisationValue = await VehiclePriceFinder.Find(veiculo.marca, veiculo.modelo, veiculo.ano);
    return indemnisationValue;

  }

}