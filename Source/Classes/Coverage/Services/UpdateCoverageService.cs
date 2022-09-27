public static class UpdateCoverageService
{
    /** <summary> Esta função altera uma cobertura no banco de dados. </summary>**/
    public static async Task<IResult> Update(int id, Cobertura cobertura, SqlConnection connectionString)
    {
        bool hasValidProperties = NullPropertyValidator.Validate(cobertura);
        if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

        double coverageValue = double.TryParse(cobertura.valor, out coverageValue) ? coverageValue : 0;

        if (cobertura.taxa_indenizacao <= 0) return Results.BadRequest(new { message = "Taxa de indenização não pode ser 0% ou menor." });
        if (cobertura.taxa_indenizacao > 100) return Results.BadRequest(new { message = "Taxa de indenização não pode ser maior que 100%." });

        if (coverageValue <= 0) return Results.BadRequest(new { message = "Valor da cobertura não pode ser 0 ou menor." });

        var coverage = await GetCoverageByIdRepository.Get(id: id, connectionString: connectionString);
        if (coverage == null) return Results.NotFound(new { message = "Cobertura não encontrada." });

        bool nameIsValid = await CoverageAlreadyExistsValidator.Validate(id: id, name: cobertura.nome, connectionString: connectionString);
        if (!nameIsValid) return Results.BadRequest(new { message = "O nome da cobertura já está sendo utiizado por outra cobertura ativa." });

        var updatedCoverage = await UpdateCoverageRepository.Update(id: id, cobertura: cobertura, connectionString: connectionString);
        if (updatedCoverage == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

        return Results.Ok(new { message = "Cobertura alterada com sucesso.", coverage = updatedCoverage });
    }
}
