static class InsertClientService
{
  /** <summary> Esta função insere um cliente no banco de dados. </summary>**/
  public static async Task<IResult> Insert(Cliente cliente, SqlConnection connectionString)
  {
    // Fazendo o telefone2 pular a verificação de nulos.
    string? originalTelefone2 = cliente.telefone2;
    if (cliente.telefone2 == "" || cliente.telefone2 == null) cliente.telefone2 = "-";

    // Verificando se alguma das propriedades do cliente é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(cliente);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Voltando telefone2 para o valor original.
    cliente.telefone2 = originalTelefone2;

    // Por padrão, o status do cliente é true.
    cliente.status = true;

    // Verificando idade do cliente. Padrão de idade é mês/dia/ano.
    int age = AgeCalculator.Calculate(cliente.data_nascimento);
    if (age < 18) return Results.BadRequest("Cliente não pode ser menor de idade.");

    // Convertendo idade para SQL Server.
    cliente.data_nascimento = SqlDateConverter.Convert(cliente.data_nascimento);

    // Verificação de CPF
    bool cpfIsValid = CpfValidation.Validate(cliente.cpf);
    cpfIsValid = StringFormatValidator.ValidateCPF(cliente.cpf);
    if (!cpfIsValid) return Results.BadRequest("O CPF informado é inválido ou está mal formatado. Lembre-se que o CPF deve estar no formato: 000.000.000-00.");

    // Verificação de CNH
    bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
    if (!cnhIsValid) return Results.BadRequest("O CNH informado é inválido.");

    // Verificação de CEP
    bool cepIsValid = await CepValidator.Validate(cliente.cep);
    if (!cepIsValid) return Results.BadRequest("O CEP informado é inválido.");

    // Verificando se o cliente já existe no banco de dados.
    bool clienteIsValid = ClientAlreadyExistsValidator.Validate(cliente, connectionString);
    if (!clienteIsValid) return Results.Conflict("Os dados deste cliente já estão cadastrados no banco de dados.");

    // Verificando se o telefone1 está formatado corretamente.
    bool telefone1IsValid = StringFormatValidator.ValidateTelefone(cliente.telefone1);
    if (!telefone1IsValid) return Results.BadRequest("O telefone1 informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999.");

    // Verificando se o telefone2 está formatado corretamente.
    bool telefone2IsValid = StringFormatValidator.ValidateTelefone(cliente.telefone2);
    if (!telefone2IsValid) return Results.BadRequest("O telefone2 informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999.");

    // Criptografando a senha do cliente.
    cliente.senha = PasswordHasher.HashPassword(cliente.senha);

    var createdClientId = InsertClientRepository.Insert(cliente: cliente, connectionString: connectionString);
    if (createdClientId == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/cliente/{createdClientId}", new { id_cliente = createdClientId });
  }
}