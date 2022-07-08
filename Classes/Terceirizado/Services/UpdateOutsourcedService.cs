public static class UpdateOutsourcedService
{
  /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
  public static IResult Update(int id, Terceirizado terceirizado, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do terceirizado é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Validando CNPJ
    bool cnpjIsValid = CnpjValidation.Validate(terceirizado.cnpj);
    cnpjIsValid = StringFormatValidator.ValidateCNPJ(terceirizado.cnpj);
    if (!cnpjIsValid) return Results.BadRequest("O CNPJ informado é inválido ou está mal formatado. Lembre-se de que o CNPJ deve estar no formato: 99.999.999/9999-99.");

    // Verificando se telefone está formatado corretamente.
    bool telefoneIsValid = StringFormatValidator.ValidateTelefone(terceirizado.telefone);
    if (!telefoneIsValid) return Results.BadRequest("O telefone informado está mal formatado. Lembre-se de que o telefone deve estar no formato (99) 99999-9999.");

    // Verificando se terceirizado existe.
    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_terceirizado from Terceirizados WHERE id_terceirizado = @Id", new { Id = id });
    if (!Exists) return Results.NotFound("Terceirizado não encontrado");

    // Verificando se o CNPJ já existe no banco de dados.
    bool cnpjExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT cnpj FROM Terceirizados WHERE cnpj = @Cnpj AND status = 'true' AND id_terceirizado != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Cnpj = terceirizado.cnpj, Id = id });
    if (cnpjExists) return Results.Conflict("O CNPJ informado já está sendo utilizado por outro terceirizado ativo.");

    // Verificando se o telefone já existe no banco de dados.
    bool telefoneExists = connectionString.QueryFirstOrDefault<bool>("SELECT telefone FROM Terceirizados WHERE telefone = @Telefone AND id_terceirizado != @Id", new { Telefone = terceirizado.telefone, Id = id });
    if (telefoneExists) return Results.Conflict("O telefone informado já está cadastrado.");

    try
    {
      connectionString.Query("UPDATE Terceirizados SET nome = @Nome, funcao = @Funcao, cnpj = @Cnpj, telefone = @Telefone, valor = @Valor WHERE id_terceirizado = @Id", new { Nome = terceirizado.nome, Funcao = terceirizado.funcao, Cnpj = terceirizado.cnpj, Telefone = terceirizado.telefone, Valor = terceirizado.valor, Id = id });

      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");
    }

  }
}
