static class VehicleFIPEValidator
{
  private static readonly HttpClient client = new HttpClient();

  /**<summary> Esta função valida se os dados do veículo existem na API da FIPE. </summary>*/
  public static async Task<bool> Validate(string brand, string model, string year)
  {
    // Pesquisar por todos os veículos na API FIPE da mesma marca do veículo.
    HttpResponseMessage brandsResponse = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas");
    string brandsString = await brandsResponse.Content.ReadAsStringAsync();
    var brands = JsonSerializer.Deserialize<VehicleBrands[]>(brandsString);

    var foundBrand = brands?.FirstOrDefault(b => b.nome == brand);
    if (foundBrand == null) return false;

    // Pesquisar na API por veículo com mesmo modelo.
    HttpResponseMessage modelsResponse = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{foundBrand.codigo}/modelos");
    string modelsString = await modelsResponse.Content.ReadAsStringAsync();
    var models = JsonSerializer.Deserialize<VehicleModels>(modelsString);

    var foundModel = models?.modelos.FirstOrDefault(modelo => modelo.nome == model);
    if (foundModel == null) return false;

    // Pesquisar na API por veículo com mesmo ano.
    HttpResponseMessage yearsResponse = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{foundBrand.codigo}/modelos/{foundModel.codigo}/anos");
    string yearsString = await yearsResponse.Content.ReadAsStringAsync();
    var years = JsonSerializer.Deserialize<VehicleYears[]>(yearsString);

    var foundYear = years?.FirstOrDefault(y => y.nome == year);
    if (foundYear == null) return false;

    return true;
  }
}