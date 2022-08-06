static class PremiumGenerator
{
  /** <summary> Esta função gera um valor de prêmio. </summary>**/
  public static decimal Generate(decimal vehicleValue, int id_cobertura, SqlConnection connectionString)
  {
    // Recuperando valor da cobertura.
    var coverage = GetOneCoverageRepository.Get(id: id_cobertura, connectionString: connectionString);
    var coverageValue = Decimal.Parse(coverage.valor) / 100;

    // O prêmio consiste em apenas 1% do valor do veículo + valor da cobertura.
    decimal premiumValue = vehicleValue * 0.01m;
    premiumValue = Math.Round(premiumValue, 2) + coverageValue;

    return premiumValue;
  }
}