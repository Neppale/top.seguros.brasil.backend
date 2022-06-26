static class IndemnisationGenerator
{
  /** <summary> Esta função gera um valor de indenização. </summary>**/
  public static decimal Generate(decimal vehicleValue)
  {

    // O valor de indenização consiste em apenas 90% do valor do veículo.
    decimal indemnisationValue = vehicleValue * 0.9m;


    return indemnisationValue;
  }

}