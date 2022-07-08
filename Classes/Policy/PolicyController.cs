public static class PolicyController
{
  public static void ActivateEndpoints(WebApplication app, string dbConnectionString)
  {
    app.MapGet("/apolice/", [Authorize] (int? pageNumber) =>
    {
      return GetAllPolicyService.Get(dbConnectionString: dbConnectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todas as apólices");

    app.MapGet("/apolice/{id:int}", [Authorize] (int id) =>
    {
      return GetOnePolicyService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar apólice específica");

    app.MapGet("/apolice/documento/{id:int}", [Authorize] (int id) =>
    {
      return GetPolicyDocumentService.Get(id: id, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar documento de apólice específica");

    app.MapGet("/apolice/usuario/{id:int}", [Authorize] (int id, int? pageNumber) =>
    {
      return GetPolicyByUserService.Get(id_usuario: id, pageNumber: pageNumber, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar apólice por usuário");

    app.MapGet("/apolice/cliente/{id:int}", [Authorize] (int id, int? pageNumber) =>
    {
      return GetPolicyByClientService.Get(id_cliente: id, pageNumber: pageNumber, dbConnectionString: dbConnectionString);
    })
    .WithName("Selecionar apólice por cliente");

    app.MapPost("/apolice/", [Authorize] (Apolice apolice) =>
    {
      return InsertPolicyService.Insert(apolice: apolice, dbConnectionString: dbConnectionString);
    })
    .WithName("Inserir apólice");

    app.MapPut("/apolice/{id:int}/{status:alpha}", [Authorize] (int id, string status) =>
    {
      return UpdatePolicyStatusService.UpdateStatus(id: id, status: status, dbConnectionString: dbConnectionString);
    })
    .WithName("Alterar status de apólice específica");

    app.MapPost("/apolice/gerar/", [Authorize] (GenerateApolice apolice) =>
    {
      return GeneratePolicyService.Generate(id_veiculo: apolice.id_veiculo, id_cliente: apolice.id_cliente, id_cobertura: apolice.id_cobertura, dbConnectionString: dbConnectionString);
    })
    .WithName("Gerar apólice");
  }
}