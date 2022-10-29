static class PremiumCalculator
{
    /** <summary> Esta função gera um valor de prêmio. </summary>**/
    public static async Task<decimal> Calculate(decimal vehicleValue, int id_cobertura, SqlConnection connectionString)
    {
        var coverage = await GetCoverageByIdRepository.Get(id: id_cobertura, connectionString: connectionString);
        var coverageValue = coverage.valor;

        decimal premiumValue = (((vehicleValue) * 0.005m) + coverageValue);
        premiumValue = Math.Round(premiumValue, 2);
        return premiumValue;
    }
}