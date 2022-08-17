public static class InsertOutsourcedService
{
  /** <summary> Esta função insere uma Terceirizado no banco de dados. </summary>**/
  public static IResult Insert(Terceirizado terceirizado, SqlConnection connectionString)
  {
    bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    terceirizado.status = true;

    bool cnpjIsValid = CnpjValidation.Validate(terceirizado.cnpj);
    bool cnpjFormatIsValid = StringFormatValidator.ValidateCNPJ(terceirizado.cnpj);
    if (!cnpjIsValid || !cnpjFormatIsValid) return Results.BadRequest("O CNPJ informado é inválido ou está mal formatado. Lembre-se de que o CNPJ deve estar no formato: 99.999.999/9999-99.");

    bool telefoneIsValid = StringFormatValidator.ValidateTelefone(terceirizado.telefone);
    if (!telefoneIsValid) return Results.BadRequest("O telefone informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999.");

    bool terceirizadoIsValid = OutsourcedAlreadyExistsValidator.Validate(terceirizado, connectionString);
    if (!terceirizadoIsValid) return Results.Conflict("Os dados deste terceirizado já estão cadastrados no banco de dados.");

    var result = InsertOutsourcedRepository.Insert(outsourced: terceirizado, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/cliente/{result}", new { id_terceirizado = result });
  }
}

