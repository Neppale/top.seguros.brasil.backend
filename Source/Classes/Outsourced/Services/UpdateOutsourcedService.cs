public static class UpdateOutsourcedService
{
  /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
  public static IResult Update(int id, Terceirizado terceirizado, SqlConnection connectionString)
  {
    bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    bool cnpjIsValid = CnpjValidation.Validate(terceirizado.cnpj);
    cnpjIsValid = StringFormatValidator.ValidateCNPJ(terceirizado.cnpj);
    if (!cnpjIsValid) return Results.BadRequest("O CNPJ informado é inválido ou está mal formatado. Lembre-se de que o CNPJ deve estar no formato: 99.999.999/9999-99.");

    bool telefoneIsValid = StringFormatValidator.ValidateTelefone(terceirizado.telefone);
    if (!telefoneIsValid) return Results.BadRequest("O telefone informado está mal formatado. Lembre-se de que o telefone deve estar no formato (99) 99999-9999.");

    var outsourced = GetOneOutsourcedRepository.Get(id: id, connectionString: connectionString);
    if (outsourced == null) return Results.NotFound("Terceirizado não encontrado");
    
    bool terceirizadoIsValid = OutsourcedAlreadyExistsValidator.Validate(id: id, outsourced: terceirizado, connectionString: connectionString);
    if (!terceirizadoIsValid) return Results.Conflict("Os dados deste terceirizado já estão cadastrados no banco de dados.");

    var result = UpdateOutsourcedRepository.Update(id: id, outsourced: terceirizado, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Ok("Terceirizado alterado com sucesso.");
  }
}
