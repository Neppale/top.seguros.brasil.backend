public static class InsertCoverageService
{
    /** <summary> Esta função insere uma cobertura no banco de dados. </summary>**/
    public static async Task<IResult> Insert(Cobertura cobertura, SqlConnection connectionString)
    {
        bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
        if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

        cobertura.status = true;
        double coverageValue = double.TryParse(cobertura.valor, out coverageValue) ? coverageValue : 0;

        if (cobertura.taxa_indenizacao <= 0) return Results.BadRequest(new { message = "Taxa de indenização não pode ser 0% ou menor." });
        if (cobertura.taxa_indenizacao > 100) return Results.BadRequest(new { message = "Taxa de indenização não pode ser maior que 100%." });

        if (coverageValue <= 0) return Results.BadRequest(new { message = "Valor da cobertura não pode ser 0 ou menor." });

        bool nameIsValid = await CoverageAlreadyExistsValidator.Validate(name: cobertura.nome, connectionString: connectionString);
        if (!nameIsValid) return Results.Conflict(new { message = "O nome da cobertura já está sendo utilizado por outra cobertura ativa." });

        var createdCoverage = await InsertCoverageRepository.Insert(cobertura: cobertura, connectionString: connectionString);
        if (createdCoverage == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

        return Results.Created($"/cobertura/{createdCoverage}", new { message = "Cobertura criada com sucesso.", coverage = createdCoverage });
    }
}
