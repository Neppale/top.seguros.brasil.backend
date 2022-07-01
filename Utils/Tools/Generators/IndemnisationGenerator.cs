static class IndemnisationGenerator
{
  /** <summary> Esta função gera um valor de indenização. </summary>**/
  public static decimal Generate(int id_cobertura, decimal vehicleValue, string dbConnectionString)
  {
    SqlConnection connection = new SqlConnection(dbConnectionString);

    // Recuperar taxa de indenização da cobertura.
    decimal taxa_indenizacao = connection.QueryFirstOrDefault<decimal>("SELECT taxa_indenizacao FROM Coberturas WHERE id_cobertura = @IdCobertura", new { IdCobertura = id_cobertura });

    // Calcular valor de indenização. Porcentagem da taxa de indenização em comparação com o valor do veículo.
    decimal indemnisationValue = vehicleValue * (taxa_indenizacao / 100);

    // Deixar o valor de indenização sempre com apenas duas casas decimais.
    indemnisationValue = Math.Round(indemnisationValue, 2);

    return indemnisationValue;
  }

}