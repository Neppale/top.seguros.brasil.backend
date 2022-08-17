static class VehicleFIPEValidator
{
  private static readonly HttpClient client = new HttpClient();

  /**<summary> Esta função valida se os dados do veículo existem na API da FIPE. </summary>*/
  public static async Task<bool> Validate(string brand, string model, string year)
  {
    // Pesquisar por todos os veículos na API FIPE da mesma marca do veículo.
    HttpResponseMessage brands = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas");
    string brandsJson = await brands.Content.ReadAsStringAsync();

    // Encontrar marca do veículo no texto do JSON.
    Regex regex = new Regex($"{{\\\"nome\\\":\\\"{brand}\\\",\\\"codigo\\\":\\\"[0-9]+\\\"}}");
    Match match = regex.Match(brandsJson);

    // Encontrar o código da marca.
    Regex regex2 = new Regex("[0-9]+");
    Match foundBrandCode = regex2.Match(match.Value);
    string brandCode = foundBrandCode.Value;

    // Pesquisar na API por veículo com mesmo modelo.
    HttpResponseMessage models = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{brandCode}/modelos");
    string modelsJson = await models.Content.ReadAsStringAsync();

    // Encontrar objeto com o modelo e código do veícudo na variável modelsJson
    Regex regex3 = new Regex($"{{\\\"nome\\\":\\\"{model}\\\",\\\"codigo\\\":[0-9]+}}");
    Match match2 = regex3.Match(modelsJson);

    // Encontrar o último numero dentro de 'match3'
    Regex regex4 = new Regex("(\\d+)(?!.*\\d)");
    Match foundModelCode = regex4.Match(match2.Value);
    string modelCode = foundModelCode.Value;

    // Pesquisar na API por veículo com mesmo ano de fabricação.
    HttpResponseMessage years = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{brandCode}/modelos/{modelCode}/anos");
    string yearsJson = await years.Content.ReadAsStringAsync();

    // Encontrar objeto com o ano de fabricação e código do veícudo na variável yearsJson
    Regex regex5 = new Regex($"{{\\\"nome\\\":\\\"{year}\\\",\\\"codigo\\\":\\\"[0-9]+-[0-9]+\\\"}}");
    Match match5 = regex5.Match(yearsJson);

    // Encontrar o código dentro de 'match5'
    Regex regex6 = new Regex("[0-9]+\\-[0-9]+");
    Match foundYearCode = regex6.Match(match5.Value);
    string yearCode = foundYearCode.Value;

    if (brandCode == "" || modelCode == "" || yearCode == "") return false;
    return true;
  }
}