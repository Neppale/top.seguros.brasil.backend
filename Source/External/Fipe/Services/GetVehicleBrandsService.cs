static class GetVehicleBrandsService
{
  public static async Task<IResult> Get()
  {
    var client = new HttpClient();
    var response = await client.GetAsync("https://parallelum.com.br/fipe/api/v1/carros/marcas");
    var content = await response.Content.ReadAsStringAsync();

    try
    {
      var brands = JsonSerializer.Deserialize<VehicleBrands[]>(content);
      return Results.Ok(brands);
    }
    catch (JsonException)
    {
      return Results.StatusCode(502);
    }
  }
}