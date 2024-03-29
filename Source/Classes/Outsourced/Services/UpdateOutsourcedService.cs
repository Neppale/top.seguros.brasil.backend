public static class UpdateOutsourcedService
{
    /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
    public static async Task<IResult> Update(int id, Terceirizado terceirizado, SqlConnection connectionString)
    {
        bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
        if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

        bool cnpjIsValid = CnpjValidation.Validate(terceirizado.cnpj);
        cnpjIsValid = StringFormatValidator.ValidateCNPJ(terceirizado.cnpj);
        if (!cnpjIsValid) return Results.BadRequest(new { message = "O CNPJ informado é inválido ou está mal formatado. Lembre-se de que o CNPJ deve estar no formato: 99.999.999/9999-99." });

        bool telefoneIsValid = StringFormatValidator.ValidateTelefone(terceirizado.telefone);
        if (!telefoneIsValid) return Results.BadRequest(new { message = "O telefone informado está mal formatado. Lembre-se de que o telefone deve estar no formato (99) 99999-9999." });

        var outsourced = await GetOutsourcedByIdRepository.Get(id: id, connectionString: connectionString);
        if (outsourced == null) return Results.NotFound(new { message = "Terceirizado não encontrado" });

        bool terceirizadoIsValid = await OutsourcedAlreadyExistsValidator.Validate(id: id, outsourced: terceirizado, connectionString: connectionString);
        if (!terceirizadoIsValid) return Results.Conflict(new { message = "Os dados deste terceirizado já estão cadastrados no banco de dados." });

        var updatedOutsourced = await UpdateOutsourcedRepository.Update(id: id, outsourced: terceirizado, connectionString: connectionString);
        if (updatedOutsourced == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

        return Results.Ok(new { message = "Terceirizado alterado com sucesso.", outsourced = updatedOutsourced });
    }
}
