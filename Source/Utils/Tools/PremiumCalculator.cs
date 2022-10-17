static class PremiumCalculator
{
    /** <summary> Esta função gera um valor de prêmio. </summary>**/
    public static async Task<decimal> Calculate(decimal vehicleValue, int id_cobertura, SqlConnection connectionString)
    {
        var coverage = await GetCoverageByIdRepository.Get(id: id_cobertura, connectionString: connectionString);
        var coverageValue = coverage.valor;
        Console.WriteLine("Coverage value: " + coverageValue);

        decimal premiumValue = (((vehicleValue) * 0.005m) + coverageValue);
        Console.WriteLine("Vehicle value: " + vehicleValue);
        Console.WriteLine("Premium value: " + premiumValue);

        premiumValue = Math.Round(premiumValue, 2);
        Console.WriteLine("Premium value rounded: " + premiumValue);

        return premiumValue;
    }
}