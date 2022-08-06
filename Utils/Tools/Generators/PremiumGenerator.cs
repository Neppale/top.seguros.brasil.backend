static class PremiumGenerator
{
  /** <summary> Esta função gera um valor de prêmio. </summary>**/
  public static decimal Generate(decimal vehicleValue, int id_cobertura, SqlConnection connectionString)
  {
    // Recuperando valor da cobertura.
    decimal coberturaValue = connectionString.QueryFirst<decimal>("SELECT valor FROM Coberturas WHERE id_cobertura = @Id", new { Id = id_cobertura });

    // O prêmio consiste em apenas 1% do valor do veículo + valor da cobertura.
    decimal premiumValue = vehicleValue * 0.01m;
    premiumValue = Math.Round(premiumValue, 2) + coberturaValue;

    return premiumValue;
  }
}