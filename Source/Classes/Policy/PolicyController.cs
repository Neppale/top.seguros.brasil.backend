public static class PolicyController
{
  public static void ActivateEndpoints(WebApplication app, SqlConnection connectionString)
  {
    app.MapGet("/apolice/", [Authorize] (int? pageNumber) =>
    {
      return GetAllPolicyService.Get(connectionString: connectionString, pageNumber: pageNumber);
    })
    .WithName("Selecionar todas as apólices");

    app.MapGet("/apolice/{id:int}", [Authorize] (int id) =>
    {
      return GetOnePolicyService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar apólice específica");

    app.MapGet("/apolice/documento/{id:int}", [Authorize] (int id) =>
    {
      return GetPolicyDocumentService.Get(id: id, connectionString: connectionString);
    })
    .WithName("Selecionar documento de apólice específica");

    app.MapGet("/apolice/usuario/{id:int}", [Authorize] (int id, int? pageNumber) =>
    {
      return GetPolicyByUserService.Get(id_usuario: id, pageNumber: pageNumber, connectionString: connectionString);
    })
    .WithName("Selecionar apólice por usuário");

    app.MapGet("/apolice/cliente/{id:int}", [Authorize] (int id, int? pageNumber) =>
    {
      return GetPolicyByClientService.Get(id_cliente: id, pageNumber: pageNumber, connectionString: connectionString);
    })
    .WithName("Selecionar apólice por cliente");

    app.MapPost("/apolice/", [Authorize] (Apolice apolice) =>
    {
      return InsertPolicyService.Insert(apolice: apolice, connectionString: connectionString);
    })
    .WithName("Inserir apólice");

    app.MapPut("/apolice/{id:int}/{status:alpha}", [Authorize] (int id, string status) =>
    {
      return UpdatePolicyStatusService.Update(id: id, status: status, connectionString: connectionString);
    })
    .WithName("Alterar status de apólice específica");

    app.MapPost("/apolice/gerar/", [Authorize] (GenerateApoliceDto apolice) =>
    {
      return GeneratePolicyService.Generate(id_veiculo: apolice.id_veiculo, id_cliente: apolice.id_cliente, id_cobertura: apolice.id_cobertura, connectionString: connectionString);
    })
    .WithName("Gerar apólice");
  }
}