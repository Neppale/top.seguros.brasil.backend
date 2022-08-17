static class InsertClientService
{
  /** <summary> Esta função insere um cliente no banco de dados. </summary>**/
  public static async Task<IResult> Insert(Cliente cliente, SqlConnection connectionString)
  {
    string? originalTelefone2 = cliente.telefone2;
    if (cliente.telefone2 == "" || cliente.telefone2 == null) cliente.telefone2 = "-";

    bool hasValidProperties = NullPropertyValidator.Validate(cliente);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    cliente.telefone2 = originalTelefone2;

    cliente.status = true;

    int age = AgeCalculator.Calculate(cliente.data_nascimento);
    if (age < 18) return Results.BadRequest("Cliente não pode ser menor de idade.");

    cliente.data_nascimento = SqlDateConverter.Convert(cliente.data_nascimento);

    bool isValidPassword = PasswordValidator.Validate(cliente.senha);
    if (!isValidPassword) return Results.BadRequest("A senha informada não corresponde aos requisitos de segurança.");

    bool cpfIsValid = CpfValidation.Validate(cliente.cpf);
    bool cpfFormatIsValid = StringFormatValidator.ValidateCPF(cliente.cpf);
    if (!cpfIsValid || !cpfFormatIsValid) return Results.BadRequest("O CPF informado é inválido ou está mal formatado. Lembre-se que o CPF deve estar no formato: 000.000.000-00.");

    bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
    if (!cnhIsValid) return Results.BadRequest("O CNH informado é inválido.");

    bool cepIsValid = await CepValidator.Validate(cliente.cep);
    if (!cepIsValid) return Results.BadRequest("O CEP informado é inválido.");

    bool clienteIsValid = ClientAlreadyExistsValidator.Validate(cliente, connectionString);
    if (!clienteIsValid) return Results.Conflict("Os dados deste cliente já estão cadastrados no banco de dados.");

    bool telefone1IsValid = StringFormatValidator.ValidateTelefone(cliente.telefone1);
    if (!telefone1IsValid) return Results.BadRequest("O telefone1 informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999.");

    bool telefone2IsValid = StringFormatValidator.ValidateTelefone(cliente.telefone2);
    if (!telefone2IsValid) return Results.BadRequest("O telefone2 informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999.");

    cliente.senha = PasswordHasher.HashPassword(cliente.senha);

    var createdClientId = InsertClientRepository.Insert(cliente: cliente, connectionString: connectionString);
    if (createdClientId == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/cliente/{createdClientId}", new { id_cliente = createdClientId });
  }
}