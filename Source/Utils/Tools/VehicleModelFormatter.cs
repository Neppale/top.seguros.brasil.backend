static class VehicleModelFormatter
{
  /** <summary>Esta função formata o modelo do veículo para ser encontrado na API da FIPE.</summary> **/
  public static string Format(string modelo)
  {
    // Se o modelo do veículo possui uma barra, adicionar outra barra para passar ma validação da FIPE.
    if (modelo.Contains("/")) modelo = modelo.Replace("/", "\\\\/");

    // Se o modelo do veículo possui um parenteses, adicionar uma barra para passar na validação da FIPE.
    if (modelo.Contains("(")) modelo = modelo.Replace("(", "\\(");
    if (modelo.Contains(")")) modelo = modelo.Replace(")", "\\)");

    return modelo;
  }
}