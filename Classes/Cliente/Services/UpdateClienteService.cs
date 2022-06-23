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
    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente FROM Clientes WHERE id_cliente = @Id", new { Id = id });
    if (!Exists) return Results.NotFound("Cliente não encontrado.");

    // Verificando se alguma das propriedades do cliente é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(cliente);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificação de CNH
    bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
    if (!cnhIsValid) return Results.BadRequest("O CPF informado é inválido.");

    // Verificação de CEP
    Task<bool> cepIsValid = CepValidator.Validate(cliente.cep);
    if (!cepIsValid.Result) return Results.BadRequest("O CEP informado é inválido.");

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