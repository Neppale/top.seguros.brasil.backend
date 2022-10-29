static class UpdateClientService
{
  /** <summary> Esta função altera um cliente no banco de dados. </summary>**/
  public static async Task<IResult> Update(int id, Cliente cliente, SqlConnection connectionString)
  {

    var client = GetClientByIdRepository.Get(id: id, connectionString: connectionString);
    if (client == null) return Results.NotFound(new { message = "Cliente não encontrado." });

    string? originalTelefone2 = cliente.telefone2;
    cliente.telefone2 = "-";

    bool hasValidProperties = NullPropertyValidator.Validate(cliente);
    if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

    cliente.telefone2 = originalTelefone2;

    int age = AgeCalculator.Calculate(cliente.data_nascimento);
    if (age < 18) return Results.BadRequest("Cliente não pode ser menor de idade.");

    bool passwordIsValid = PasswordValidator.Validate(cliente.senha);
    if (!passwordIsValid) return Results.BadRequest("A senha informada não corresponde aos requisitos de segurança.");

    bool telefone1IsValid = StringFormatValidator.ValidateTelefone(cliente.telefone1);
    if (!telefone1IsValid) return Results.BadRequest("O telefone 1 informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999.");

    bool telefone2IsValid = StringFormatValidator.ValidateTelefone(cliente.telefone2);
    if (!telefone2IsValid) return Results.BadRequest("O telefone 2 informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999.");

    bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
    if (!cnhIsValid) return Results.BadRequest("O CPF informado é inválido.");

    bool cepIsValid = await CepValidator.Validate(cliente.cep);
    if (!cepIsValid) return Results.BadRequest("O CEP informado é inválido.");

    bool clientIsValid = ClientAlreadyExistsValidator.Validate(id: id, cliente: cliente, connectionString: connectionString);
    if (!clientIsValid) return Results.BadRequest("O CNH ou e-mail informado já está sendo utilizado em outra conta.");

    cliente.senha = PasswordHasher.HashPassword(cliente.senha);

    var updatedClient = await UpdateClientRepository.Update(id: id, cliente: cliente, connectionString: connectionString);
    if (updatedClient == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.Ok(new { message = "Cliente atualizado com sucesso.", client = updatedClient });
  }
}