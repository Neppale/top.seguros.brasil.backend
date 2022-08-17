static class IndemnisationCalculator
{
  /** <summary> Esta função gera um valor de indenização. </summary>**/
  public static decimal Calculate(int id_cobertura, decimal vehicleValue, SqlConnection connectionString)
  {
    var coverage = GetOneCoverageRepository.Get(id: id_cobertura, connectionString: connectionString);
    decimal indemnisationTax = Decimal.Parse(coverage.taxa_indenizacao.ToString()) / 100;

    decimal indemnisationValue = vehicleValue * indemnisationTax;
    indemnisationValue = Math.Round(indemnisationValue, 2);

    return indemnisationValue;
  }
}