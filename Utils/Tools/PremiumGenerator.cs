static class PremiumGenerator
{
  /** <summary> Esta função gera um valor de prêmio. </summary>**/
  public static async Task<decimal> Generate(int id_veiculo, int id_cobertura, string dbConnectionString)
  {
    SqlConnection connection = new SqlConnection(dbConnectionString);

    // Recuperar dados do veículo no banco.
    Veiculo veiculo = connection.QueryFirst<Veiculo>("SELECT * FROM Veiculos WHERE id_veiculo = @id", new { id = id_veiculo });

    // Recuperar valor de cobertura no banco.
    decimal coberturaPrice = connection.QueryFirst<decimal>("SELECT valor FROM Coberturas WHERE id_cobertura = @id", new { id = id_cobertura });

    // O prêmio consiste em apenas 1% do valor do veículo + valor da cobertura.
    decimal value = await FipeAPIAccess.GetValue(veiculo.marca, veiculo.modelo, veiculo.ano);
    decimal premiumValue = value * 0.01m;
    premiumValue = Math.Round(premiumValue, 2) + coberturaPrice;

    return premiumValue;
  }
}