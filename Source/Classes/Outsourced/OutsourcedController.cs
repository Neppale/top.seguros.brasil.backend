public static class OutsourcedController
{
    public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString)
    {
        app.MapGet("/terceirizado/", [Authorize] async (int? pageNumber, int? size) =>
        {
            return await GetAllOutsourcedService.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);
        })
        .WithName("Selecionar todos os terceirizados");

        app.MapGet("/terceirizado/{id:int}", [Authorize] async (int id) =>
        {
            return await GetOutsourcedByIdService.Get(id: id, connectionString: connectionString);
        })
        .WithName("Selecionar terceirizado específico");

        app.MapPost("/terceirizado/", [Authorize] async (Terceirizado terceirizado) =>
        {
            return await InsertOutsourcedService.Insert(terceirizado: terceirizado, connectionString: connectionString);
        })
        .WithName("Inserir terceirizado");

        app.MapPut("/terceirizado/{id:int}", [Authorize] async (int id, Terceirizado terceirizado) =>
        {
            return await UpdateOutsourcedService.Update(id: id, terceirizado: terceirizado, connectionString: connectionString);
        })
        .WithName("Alterar terceirizado específico");

        app.MapDelete("/terceirizado/{id:int}", [Authorize] async (int id) =>
        {
            return await DeleteOutsourcedService.Delete(id: id, connectionString: connectionString);
        })
        .WithName("Desativar terceirizado específico");
    }

}
