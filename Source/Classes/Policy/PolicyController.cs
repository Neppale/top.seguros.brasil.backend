public static class PolicyController
{
    public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString)
    {
        app.MapGet("/apolice/", [Authorize] async (int? pageNumber, int? size) =>
        {
            return await GetAllPolicyService.Get(connectionString: connectionString, pageNumber: pageNumber, size: size);
        })
        .WithName("Selecionar todas as apólices");

        app.MapGet("/apolice/{id:int}", [Authorize] async (int id) =>
        {
            return await GetPolicyByIdService.Get(id: id, connectionString: connectionString);
        })
        .WithName("Selecionar apólice específica");

        app.MapGet("/apolice/documento/{id:int}", [Authorize] async (int id) =>
        {
            return await GetPolicyDocumentService.Get(id: id, connectionString: connectionString);
        })
        .WithName("Selecionar documento de apólice específica");

        app.MapGet("/apolice/usuario/{id:int}", [Authorize] async (int id, int? pageNumber, int? size, string? search) =>
        {
            return await GetPolicyByUserService.Get(id_usuario: id, pageNumber: pageNumber, connectionString: connectionString, size: size, search: search);
        })
        .WithName("Selecionar apólice por usuário");

        app.MapGet("/apolice/cliente/{id:int}", [Authorize] async (int id, int? pageNumber, int? size) =>
        {
            return await GetPolicyByClientService.Get(id_cliente: id, pageNumber: pageNumber, connectionString: connectionString, size: size);
        })
        .WithName("Selecionar apólice por cliente");

        app.MapPost("/apolice/", [Authorize] async (Apolice apolice) =>
        {
            return await InsertPolicyService.Insert(apolice: apolice, connectionString: connectionString);
        })
        .WithName("Inserir apólice");

        app.MapPut("/apolice/{id:int}/{status:alpha}", [Authorize] async (int id, string status) =>
        {
            return await UpdatePolicyStatusService.Update(id: id, status: status, connectionString: connectionString);
        })
        .WithName("Alterar status de apólice específica");

        app.MapPost("/apolice/gerar/", [Authorize] async (GenerateApoliceDto apolice) =>
        {
            return await GeneratePolicyService.Generate(id_veiculo: apolice.id_veiculo, id_cliente: apolice.id_cliente, id_cobertura: apolice.id_cobertura, connectionString: connectionString);
        })
        .WithName("Gerar apólice");

    }
}