using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
using DocumentValidator;
static class InsertClienteService
{
  /** <summary> Esta função insere um cliente no banco de dados. </summary>**/
  public static IResult Insert(Cliente cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades do cliente é nula ou vazia.
      //TODO: Telefone2 pode ser nulo. Precisa ser ignorado por essa verificação.
      bool hasValidProperties = NullPropertyValidator.Validate(cliente);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      // Verificação de CPF
      bool cpfIsValid = CpfValidation.Validate(cliente.cpf);
      if (!cpfIsValid) return Results.BadRequest("O CPF informado é inválido.");

      // Verificação de CNH
      bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
      if (!cnhIsValid) return Results.BadRequest("O CPF informado é inválido.");

      // Verificação de CEP
      Task<bool> cepIsValid = CepValidator.Validate(cliente.cep);
      if (!cepIsValid.Result) return Results.BadRequest("O CEP informado é inválido.");

      // Criptografando a senha do cliente.
      cliente.senha = PasswordHasher.HashPassword(cliente.senha);

      var data = connectionString.Query<Cliente>("INSERT INTO Clientes (email, senha, nome_completo, cpf, cnh, cep, data_nascimento, telefone1, telefone2, status) VALUES (@Email, @Senha, @Nome, @Cpf, @Cnh, @Cep, @DataNascimento, @Telefone1, @Telefone2, @Status)",
        new { Email = cliente.email, Senha = cliente.senha, Nome = cliente.nome_completo, Cpf = cliente.cpf, Cnh = cliente.cnh, Cep = cliente.cep, DataNascimento = cliente.data_nascimento, Telefone1 = cliente.telefone1, Telefone2 = cliente.telefone2, Status = cliente.status });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}