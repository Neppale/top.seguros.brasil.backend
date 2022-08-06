static class IndemnisationGenerator
{
  /** <summary> Esta função gera um valor de indenização. </summary>**/
  public static decimal Generate(int id_cobertura, decimal vehicleValue, SqlConnection connectionString)
  {
    // Recuperar taxa de indenização da cobertura.
    var coverage = GetOneCoverageRepository.Get(id: id_cobertura, connectionString: connectionString);
    decimal indemnisationTax = Decimal.Parse(coverage.taxa_indenizacao.ToString()) / 100;

    // Calcular valor de indenização. Porcentagem da taxa de indenização em comparação com o valor do veículo.
    decimal indemnisationValue = vehicleValue * indemnisationTax;

    // Deixar o valor de indenização sempre com apenas duas casas decimais.
    indemnisationValue = Math.Round(indemnisationValue, 2);

    return indemnisationValue;
  }

}