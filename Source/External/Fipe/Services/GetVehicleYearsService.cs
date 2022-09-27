static class GetVehicleYearsService
{
    public static async Task<IResult> Get(int modelId, int brandId)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{brandId}/modelos/{modelId}/anos");
        var content = await response.Content.ReadAsStringAsync();

        try
        {
            var years = JsonSerializer.Deserialize<VehicleYears[]>(content);
            return Results.Ok(years);
        }
        catch (JsonException)
        {
            return Results.StatusCode(502);
        }
    }
}