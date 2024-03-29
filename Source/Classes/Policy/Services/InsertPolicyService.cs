static class InsertPolicyService
{
    /** <summary> Esta função insere uma apólice no banco de dados. </summary>**/
    public static async Task<IResult> Insert(Apolice apolice, SqlConnection connectionString)
    {
        apolice.documento = "-";

        bool hasValidProperties = NullPropertyValidator.Validate(apolice);
        if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

        apolice.status = "Em Analise";

        if (apolice.indenizacao <= 0) return Results.BadRequest(new { message = "Valor de indenização não pode ser menor ou igual a zero." });
        if (apolice.premio <= 0) return Results.BadRequest(new { message = "Valor de prêmio não pode ser menor ou igual a zero." });

        apolice.indenizacao = Decimal.Parse(apolice.indenizacao.ToString().Replace(",", "."));
        apolice.premio = Decimal.Parse(apolice.premio.ToString().Replace(",", "."));

        var cliente = await GetClientByIdRepository.Get(id: apolice.id_cliente, connectionString: connectionString);
        if (cliente == null) return Results.NotFound(new { message = "Cliente não encontrado." });

        var vehicle = await GetVehicleByIdRepository.Get(id: apolice.id_veiculo, connectionString: connectionString);
        if (vehicle == null) return Results.NotFound(new { message = "Veículo não encontrado." });

        bool vehicleBelongsToClient = await ClientVehicleValidator.Validate(id_cliente: apolice.id_cliente, id_veiculo: apolice.id_veiculo, connectionString: connectionString);
        if (!vehicleBelongsToClient) return Results.BadRequest(new { message = "Veículo não pertence ao cliente." });

        var coverage = await GetCoverageByIdRepository.Get(id: apolice.id_cobertura, connectionString: connectionString);
        if (coverage == null) return Results.NotFound(new { message = "Cobertura não encontrada." });

        var user = await GetUserByIdRepository.Get(id: apolice.id_usuario, connectionString: connectionString);
        if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });

        apolice.data_inicio = SqlDateConverter.ConvertToSave(apolice.data_inicio);
        if (apolice.data_inicio == "0000-00-00") return Results.BadRequest(new { message = "Formato de data inválido. Formato correto: yyyy-MM-dd" });

        apolice.data_fim = SqlDateConverter.ConvertToSave(apolice.data_fim);
        if (apolice.data_fim == "0000-00-00") return Results.BadRequest(new { message = "Formato de data inválido. Formato correto: yyyy-MM-dd" });

        var createdPolicy = await InsertPolicyRepository.Insert(apolice: apolice, connectionString: connectionString);
        if (createdPolicy == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });
        apolice.id_apolice = createdPolicy.id_apolice;

        var document = await PolicyDocumentGenerator.Generate(apolice: apolice, connectionString: connectionString);
        if (document == null) return Results.Created($"/apolice/{createdPolicy.id_apolice}", new { message = "Apólice criada com sucesso, mas houve um erro ao gerar o documento." });

        var base64Document = DocumentConverter.Encode(document);
        await InsertPolicyDocumentRepository.Insert(id: createdPolicy.id_apolice, document: base64Document, connectionString: connectionString);
        await NotifyUserRepository.Notify(id: apolice.id_usuario, connectionString: connectionString);

        return Results.Created($"/apolice/{createdPolicy.id_apolice}", new { message = "Apólice criada com sucesso.", policy = createdPolicy });
    }
}
