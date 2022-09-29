static class IndemnisationCalculator
{
    /** <summary> Esta função gera um valor de indenização. </summary>**/
    public static async Task<decimal> Calculate(int id_cobertura, decimal vehicleValue, SqlConnection connectionString)
    {
        // TODO: This is wrong on production. Log every step of the process.
        var coverage = await GetCoverageByIdRepository.Get(id: id_cobertura, connectionString: connectionString);
        decimal indemnisationTax = Decimal.Parse(coverage.taxa_indenizacao.ToString()) / 100;
        Console.WriteLine("Indemnisation tax: " + indemnisationTax);

        decimal indemnisationValue = vehicleValue * indemnisationTax;
        Console.WriteLine("Indemnisation value: " + indemnisationValue);

        indemnisationValue = Math.Round(indemnisationValue, 2);
        Console.WriteLine("Indemnisation value rounded: " + indemnisationValue);

        return indemnisationValue;
    }
}