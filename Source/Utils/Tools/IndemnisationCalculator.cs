static class IndemnisationCalculator
{
  /** <summary> Esta função gera um valor de indenização. </summary>**/
  public static async Task<decimal> Calculate(int id_cobertura, decimal vehicleValue, SqlConnection connectionString)
  {
    var coverage = await GetCoverageByIdRepository.Get(id: id_cobertura, connectionString: connectionString);
    decimal indemnisationTax = coverage.taxa_indenizacao;

    decimal indemnisationValue = (vehicleValue * (indemnisationTax / 100));
    indemnisationValue = Math.Round(indemnisationValue, 2);
    return indemnisationValue;
  }
}