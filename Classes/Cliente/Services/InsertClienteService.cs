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

      // Fazendo o telefone2 pular a validação.
      if (cliente.telefone2 == "" || cliente.telefone2 == null) cliente.telefone2 = "-";

      // Verificando se alguma das propriedades do cliente é nula ou vazia.
      bool hasValidProperties = NullPropertyValidator.Validate(cliente);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      // Voltando telefone2 para o valor original.
      cliente.telefone2 = cliente.telefone2.Replace("-", "");

      // Verificação de CPF
      bool cpfIsValid = CpfValidation.Validate(cliente.cpf);
      if (!cpfIsValid) return Results.BadRequest("O CPF informado é inválido.");

      // Verificação de CNH
      bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
      if (!cnhIsValid) return Results.BadRequest("O CPF informado é inválido.");

      // Verificação de CEP
      Task<bool> cepIsValid = CepValidator.Validate(cliente.cep);
      if (!cepIsValid.Result) return Results.BadRequest("O CEP informado é inválido.");

      // Verificando se o cliente já existe no banco de dados.
      bool clienteIsValid = ClienteAlreadyExistsValidator.Validate(cliente, dbConnectionString);
      if (!clienteIsValid) return Results.Conflict("Os dados deste cliente já estão cadastrados no banco de dados.");

      // Criptografando a senha do cliente.
      cliente.senha = PasswordHasher.HashPassword(cliente.senha);

      connectionString.Query<Cliente>("INSERT INTO Clientes (email, senha, nome_completo, cpf, cnh, cep, data_nascimento, telefone1, telefone2) VALUES (@Email, @Senha, @Nome, @Cpf, @Cnh, @Cep, @DataNascimento, @Telefone1, @Telefone2)",
       new { Email = cliente.email, Senha = cliente.senha, Nome = cliente.nome_completo, Cpf = cliente.cpf, Cnh = cliente.cnh, Cep = cliente.cep, DataNascimento = cliente.data_nascimento, Telefone1 = cliente.telefone1, Telefone2 = cliente.telefone2 });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}