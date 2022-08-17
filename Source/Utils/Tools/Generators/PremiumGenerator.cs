static class PremiumGenerator
{
  /** <summary> Esta função gera um valor de prêmio. </summary>**/
  public static decimal Generate(decimal vehicleValue, int id_cobertura, SqlConnection connectionString)
  {
    // Recuperando valor da cobertura.
    var coverage = GetOneCoverageRepository.Get(id: id_cobertura, connectionString: connectionString);
    var coverageValue = Decimal.Parse(coverage.valor) / 100;

    // O prêmio consiste em apenas 0.5% do valor do veículo + valor da cobertura dividido por 2.
    decimal premiumValue = ((vehicleValue * 0.005m) + coverageValue) / 2;
    premiumValue = Math.Round(premiumValue, 2);

    return premiumValue;
  }
}