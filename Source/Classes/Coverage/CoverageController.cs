public static class CoverageController
{
    public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString)
    {
        app.MapGet("/cobertura/", [Authorize] async (int? pageNumber, int? size, string? search) =>
        {
            return await GetAllCoverageService.Get(connectionString: connectionString, pageNumber: pageNumber, size: size, search: search);
        })
        .WithName("Selecionar todas as coberturas");

        app.MapGet("/cobertura/{id:int}", [Authorize] async (int id) =>
        {
            return await GetCoverageByIdService.Get(id: id, connectionString: connectionString);
        })
        .WithName("Selecionar cobertura específica");

        app.MapPost("/cobertura/", [Authorize] async (Cobertura cobertura) =>
        {
            return await InsertCoverageService.Insert(cobertura: cobertura, connectionString: connectionString);
        })
        .WithName("Inserir cobertura");

        app.MapPut("/cobertura/{id:int}", [Authorize] async (int id, Cobertura cobertura) =>
        {
            return await UpdateCoverageService.Update(id: id, cobertura: cobertura, connectionString: connectionString);
        })
        .WithName("Alterar cobertura específica");

        app.MapDelete("/cobertura/{id:int}", [Authorize] async (int id) =>
        {
            return await DeleteCoverageService.Delete(id: id, connectionString: connectionString);
        })
        .WithName("Desativar cobertura específica");
    }
}
