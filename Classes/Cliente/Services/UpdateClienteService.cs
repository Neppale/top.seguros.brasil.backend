using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
using DocumentValidator;
static class UpdateClienteService
{
  /** <summary> Esta função altera um cliente no banco de dados. </summary>**/
  public static IResult Update(int id, Cliente cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se o cliente existe.
    bool storedCliente = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente FROM Clientes WHERE id_cliente = @Id", new { Id = id });
    if (!storedCliente) return Results.NotFound("Cliente não encontrado.");

    // Verificando se alguma das propriedades do cliente é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(cliente);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificação de CNH
    bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
    if (!cnhIsValid) return Results.BadRequest("O CPF informado é inválido.");

    // Verificação de CEP
    Task<bool> cepIsValid = CepValidator.Validate(cliente.cep);
    if (!cepIsValid.Result) return Results.BadRequest("O CEP informado é inválido.");

    // Verificando se CNH já existe em outra conta no banco de dados.
    bool cnhAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT cnh FROM Clientes WHERE cnh = @Cnh AND id_cliente != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Cnh = cliente.cnh, Id = id });
    if (cnhAlreadyExists) return Results.BadRequest("A CNH informada já está sendo utilizada em outra conta.");

    // Verificando se o e-mail já existe em outra conta no banco de dados.
    bool emailAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT email FROM Clientes WHERE email = @Email AND id_cliente != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Email = cliente.email, Id = id });
    if (emailAlreadyExists) return Results.BadRequest("O e-mail informado já está sendo utilizado em outra conta.");

    // Criptografando a senha do cliente.
    cliente.senha = PasswordHasher.HashPassword(cliente.senha);

    try
    {
      connectionString.Query("UPDATE Clientes SET email = @Email, senha = @Senha, nome_completo = @Nome, cnh = @Cnh, cep = @Cep, data_nascimento = @DataNascimento, telefone1 = @Telefone1, telefone2 = @Telefone2 WHERE id_cliente = @Id",
        new { Email = cliente.email, Senha = cliente.senha, Nome = cliente.nome_completo, Cnh = cliente.cnh, Cep = cliente.cep, DataNascimento = cliente.data_nascimento, Telefone1 = cliente.telefone1, Telefone2 = cliente.telefone2, Id = id });

      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}