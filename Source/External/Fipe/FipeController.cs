static class FipeController
{
  public static void ActivateEndpoints(WebApplication app)
  {
    app.MapGet("/fipe/marcas", async () =>
    {
      return await GetVehicleBrandsService.Get();
    });

    app.MapGet("/fipe/marcas/{brandId}/modelos", async (int brandId) =>
{
  return await GetVehicleModelsService.Get(brandId);
});

    app.MapGet("/fipe/marcas/{brandId}/modelos/{modelId}/anos", async (int modelId, int brandId) =>
    {
      return await GetVehicleYearsService.Get(modelId: modelId, brandId: brandId);
    });
  }
}