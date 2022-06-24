static class InsertClienteService
{
  /** <summary> Esta função insere um cliente no banco de dados. </summary>**/
  public static IResult Insert(Cliente cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

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

    // Verificação de CPF
    bool cpfIsValid = CpfValidation.Validate(cliente.cpf);
    cpfIsValid = StringFormatValidator.ValidateCPF(cliente.cpf);
    if (!cpfIsValid) return Results.BadRequest("O CPF informado é inválido ou está mal formatado. Lembre-se que o CPF deve estar no formato: 000.000.000-00.");

    // Verificação de CNH
    bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
    if (!cnhIsValid) return Results.BadRequest("O CNH informado é inválido.");

    // Verificação de CEP
    Task<bool> cepIsValid = CepValidator.Validate(cliente.cep);
    if (!cepIsValid.Result) return Results.BadRequest("O CEP informado é inválido.");

    // Verificando se o cliente já existe no banco de dados.
    bool clienteIsValid = ClienteAlreadyExistsValidator.Validate(cliente, dbConnectionString);
    if (!clienteIsValid) return Results.BadRequest("Os dados deste cliente já estão cadastrados no banco de dados.");

    // Verificando se o telefone1 já existe no banco de dados.
    bool telefone1IsValid = StringFormatValidator.ValidateTelefone(cliente.telefone1);
    if (!telefone1IsValid) return Results.BadRequest("O telefone1 informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999.");

    // Verificando se o telefone2 já existe no banco de dados.
    bool telefone2IsValid = StringFormatValidator.ValidateTelefone(cliente.telefone2);
    if (!telefone2IsValid) return Results.BadRequest("O telefone2 informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999.");

    // Criptografando a senha do cliente.
    cliente.senha = PasswordHasher.HashPassword(cliente.senha);

    try
    {
      connectionString.Query<Cliente>("INSERT INTO Clientes (email, senha, nome_completo, cpf, cnh, cep, data_nascimento, telefone1, telefone2) VALUES (@Email, @Senha, @Nome, @Cpf, @Cnh, @Cep, @DataNascimento, @Telefone1, @Telefone2)", new { Email = cliente.email, Senha = cliente.senha, Nome = cliente.nome_completo, Cpf = cliente.cpf, Cnh = cliente.cnh, Cep = cliente.cep, DataNascimento = cliente.data_nascimento, Telefone1 = cliente.telefone1, Telefone2 = cliente.telefone2 });

      // Retornando o id do cliente criado.
      int createdClienteId = connectionString.QueryFirstOrDefault<int>("SELECT id_cliente FROM Clientes WHERE email = @Email", new { Email = cliente.email });

      return Results.Created($"/cliente/{createdClienteId}", new { id_cliente = createdClienteId });
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}