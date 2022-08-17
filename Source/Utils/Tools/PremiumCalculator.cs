static class PremiumCalculator
{
  /** <summary> Esta função gera um valor de prêmio. </summary>**/
  public static decimal Calculate(decimal vehicleValue, int id_cobertura, SqlConnection connectionString)
  {
    var coverage = GetOneCoverageRepository.Get(id: id_cobertura, connectionString: connectionString);
    var coverageValue = Decimal.Parse(coverage.valor) / 100;

    decimal premiumValue = ((vehicleValue * 0.01m) + coverageValue);
    premiumValue = Math.Round(premiumValue, 2);

    return premiumValue;
  }
}