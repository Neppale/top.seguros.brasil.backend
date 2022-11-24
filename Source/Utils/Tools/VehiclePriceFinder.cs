static class VehiclePriceFinder
{
    private static readonly HttpClient client = new HttpClient();

    /** <summary> Esta função recupera o valor do veículo da tabela FIPE. </summary>**/
    public static async Task<decimal> Find(string brand, string model, string year)
    {
        // Pesquisar por todos os veículos na API FIPE da mesma marca do veículo.
        HttpResponseMessage brandsResponse = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas");
        string brandsJson = await brandsResponse.Content.ReadAsStringAsync();
        var brands = JsonSerializer.Deserialize<VehicleBrands[]>(brandsJson);

        var foundBrand = brands?.FirstOrDefault(b => b.nome == brand);
        if (foundBrand == null) return 0;

        // Pesquisar na API por veículo com mesmo modelo.
        HttpResponseMessage modelsResponse = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{foundBrand.codigo}/modelos");
        string modelsJson = await modelsResponse.Content.ReadAsStringAsync();
        var models = JsonSerializer.Deserialize<VehicleModels>(modelsJson);

        var foundModel = models?.modelos.FirstOrDefault(modelo => modelo.nome == model);
        if (foundModel == null) return 0;

        // Pesquisar na API por veículo com mesmo ano.
        HttpResponseMessage yearsResponse = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{foundBrand.codigo}/modelos/{foundModel.codigo}/anos");
        string yearsJson = await yearsResponse.Content.ReadAsStringAsync();
        var years = JsonSerializer.Deserialize<VehicleYears[]>(yearsJson);

        var foundYear = years?.FirstOrDefault(y => y.nome == year);
        if (foundYear == null) return 0;

        // Pesquisar na API por valor do veículo.
        HttpResponseMessage vehicleDataResponse = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{foundBrand.codigo}/modelos/{foundModel.codigo}/anos/{foundYear.codigo}");
        string vehicleData = await vehicleDataResponse.Content.ReadAsStringAsync();

        var price = JsonSerializer.Deserialize<VehiclePrice>(vehicleData)!;
        // Remover tudo que não seja numero, e coloque duas casas decimais no fim.
        var priceValue = decimal.Parse(Regex.Replace(price.Valor, "[^0-9]", ""), NumberStyles.AllowDecimalPoint) / 100;

        return priceValue;
    }
}