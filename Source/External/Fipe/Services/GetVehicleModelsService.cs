static class GetVehicleModelsService
{
    public static async Task<IResult> Get(int brandId)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{brandId}/modelos");
        var content = await response.Content.ReadAsStringAsync();

        try
        {
            var models = JsonSerializer.Deserialize<VehicleModels>(content);
            var formattedModels = models?.modelos;
            return Results.Ok(formattedModels);
        }
        catch (JsonException)
        {
            return Results.StatusCode(502);
        }
    }
}

