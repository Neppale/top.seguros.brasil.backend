static class VehicleModelUnformatter
{
  /** <summary>Esta função formata o modelo do veículo para ser exibido corretamente.</summary> **/
  public static string Unformat(string modelo)
  {
    // Se o modelo do veículo possui barras, remover todas as barras.
    if (modelo.Contains("\\\\/")) modelo = modelo.Replace(@"\", "");

    return modelo;
  }
}