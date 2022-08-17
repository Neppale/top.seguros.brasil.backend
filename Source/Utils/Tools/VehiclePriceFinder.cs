static class VehiclePriceFinder
{
  private static readonly HttpClient client = new HttpClient();

  /** <summary> Esta função recupera o valor do veículo da tabela FIPE. </summary>**/
  public static async Task<decimal> Find(string brand, string model, string year)
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

    // Pesquisar na API por veículo com mesmo tipo de combustível e ano
    HttpResponseMessage veiculoFipeJson = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{brandCode}/modelos/{modelCode}/anos/{yearCode}");
    string veiculoJson = await veiculoFipeJson.Content.ReadAsStringAsync();

    // Encontrar o atributo 'Valor' do veículo na variável veiculoFipeJsonString
    Regex regex7 = new Regex("\"Valor\":\"R\\$ [0-9]+.[0-9]+,[0-9]+\"");
    Match foundveiculoFipeValue = regex7.Match(veiculoJson);
    string veiculoFipeValue = foundveiculoFipeValue.Value;

    // Encontrar o valor dentro de 'veiculoFipeJsonStringValue'
    Regex regex8 = new Regex("[0-9]+.[0-9]+,[0-9]+");
    Match foundVeiculoValue = regex8.Match(veiculoFipeValue);
    string veiculoValue = foundVeiculoValue.Value;

    // Remover o 'R$' e o ',' do valor, trocar o ',' por '.' e converter para decimal.
    veiculoValue = veiculoValue.Replace("R$ ", "").Replace(".", "");
    decimal value = decimal.Parse(veiculoValue);

    return value;
  }
}