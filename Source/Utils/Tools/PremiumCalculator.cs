static class PremiumCalculator
{
    /** <summary> Esta função gera um valor de prêmio. </summary>**/
    public static async Task<decimal> Calculate(decimal vehicleValue, int id_cobertura, SqlConnection connectionString)
    {
        // TODO: This is wrong on production. Log every step of the process.
        var coverage = await GetCoverageByIdRepository.Get(id: id_cobertura, connectionString: connectionString);
        var coverageValue = Decimal.Parse(coverage.valor) / 100;
        Console.WriteLine("Coverage value: " + coverageValue);

        decimal premiumValue = (((vehicleValue) * 0.005m) + coverageValue);
        Console.WriteLine("Premium value: " + premiumValue);

        premiumValue = Math.Round(premiumValue, 2);
        Console.WriteLine("Premium value rounded: " + premiumValue);

        return premiumValue;
    }
}