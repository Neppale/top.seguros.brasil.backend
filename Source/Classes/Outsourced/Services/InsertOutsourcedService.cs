public static class InsertOutsourcedService
{
  /** <summary> Esta função insere uma Terceirizado no banco de dados. </summary>**/
  public static async Task<IResult> Insert(Terceirizado terceirizado, SqlConnection connectionString)
  {
    bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
    if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

    terceirizado.status = true;

    bool cnpjIsValid = CnpjValidation.Validate(terceirizado.cnpj);
    bool cnpjFormatIsValid = StringFormatValidator.ValidateCNPJ(terceirizado.cnpj);
    if (!cnpjIsValid || !cnpjFormatIsValid) return Results.BadRequest(new { message = "O CNPJ informado é inválido ou está mal formatado. Lembre-se de que o CNPJ deve estar no formato: 99.999.999/9999-99." });

    bool telefoneIsValid = StringFormatValidator.ValidateTelefone(terceirizado.telefone);
    if (!telefoneIsValid) return Results.BadRequest(new { message = "O telefone informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999." });

    bool terceirizadoIsValid = await OutsourcedAlreadyExistsValidator.Validate(terceirizado, connectionString);
    if (!terceirizadoIsValid) return Results.Conflict(new { message = "Os dados deste terceirizado já estão cadastrados no banco de dados." });

    var createdOutsourced = await InsertOutsourcedRepository.Insert(outsourced: terceirizado, connectionString: connectionString);
    if (createdOutsourced == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.Created($"/terceirizado/{createdOutsourced.id_terceirizado}", new { message = "Terceirizado criado com sucesso.", outsourced = createdOutsourced });
  }
}

