static class InsertClientService
{
    /** <summary> Esta função insere um cliente no banco de dados. </summary>**/
    public static async Task<IResult> Insert(Cliente cliente, SqlConnection connectionString)
    {
        string? originalTelefone2 = cliente.telefone2;
        if (cliente.telefone2 == "" || cliente.telefone2 == null) cliente.telefone2 = "-";

        bool hasValidProperties = NullPropertyValidator.Validate(cliente);
        if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

        cliente.telefone2 = originalTelefone2;

        cliente.status = true;

        int age = AgeCalculator.Calculate(cliente.data_nascimento);
        if (age < 18) return Results.BadRequest(new { message = "Cliente não pode ser menor de idade." });

        bool isValidPassword = PasswordValidator.Validate(cliente.senha);
        if (!isValidPassword) return Results.BadRequest(new { message = "A senha informada não corresponde aos requisitos de segurança." });

        bool cpfIsValid = CpfValidation.Validate(cliente.cpf);
        bool cpfFormatIsValid = StringFormatValidator.ValidateCPF(cliente.cpf);
        if (!cpfIsValid || !cpfFormatIsValid) return Results.BadRequest(new { message = "O CPF informado é inválido ou está mal formatado. Lembre-se que o CPF deve estar no formato: 000.000.000-00." });

        bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
        if (!cnhIsValid) return Results.BadRequest(new { message = "O CNH informado é inválido." });

        bool cepIsValid = await CepValidator.Validate(cliente.cep);
        if (!cepIsValid) return Results.BadRequest(new { message = "O CEP informado é inválido." });

        bool clienteIsValid = ClientAlreadyExistsValidator.Validate(cliente, connectionString);
        if (!clienteIsValid) return Results.Conflict(new { message = "Os dados deste cliente já estão cadastrados no banco de dados." });

        bool newAccount = ClientIsDeletedValidator.Validate(cliente.cpf, connectionString);
        if (!newAccount) return Results.Conflict(new { message = "Não foi possível realizar o cadastro deste usuário." });

        bool telefone1IsValid = StringFormatValidator.ValidateTelefone(cliente.telefone1);
        if (!telefone1IsValid) return Results.BadRequest(new { message = "O telefone1 informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999." });

        bool telefone2IsValid = StringFormatValidator.ValidateTelefone(cliente.telefone2);
        if (!telefone2IsValid) return Results.BadRequest(new { message = "O telefone2 informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999." });

        cliente.senha = PasswordHasher.HashPassword(cliente.senha);
        cliente.data_nascimento = SqlDateConverter.ConvertToSave(cliente.data_nascimento);
        if (cliente.data_nascimento == "0000-00-00") return Results.BadRequest(new { message = "Formato de data inválido. Formato correto: yyyy-MM-dd" });

        var createdClient = await InsertClientRepository.Insert(cliente: cliente, connectionString: connectionString);
        if (createdClient == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

        return Results.Created($"/cliente/{createdClient}", new { message = "Cliente criado com sucesso.", client = createdClient });
    }
}