using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
using DocumentValidator;
public static class InsertTerceirizadoService
{
  /** <summary> Esta função insere uma Terceirizado no banco de dados. </summary>**/
  public static IResult Insert(Terceirizado terceirizado, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do Terceirizado é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Validando CNPJ
    bool cnpjIsValid = CnpjValidation.Validate(terceirizado.cnpj);
    if (!cnpjIsValid) return Results.BadRequest("O CNPJ informado é inválido.");

    // Verificando se o CNPJ já existe no banco de dados.
    bool cnpjExists = connectionString.QueryFirstOrDefault<bool>("SELECT cnpj FROM Terceirizados WHERE cnpj = @Cnpj", new { Cnpj = terceirizado.cnpj });
    if (cnpjExists) return Results.Conflict("O CNPJ informado já está cadastrado.");

    // Verificando se o telefone já existe no banco de dados.
    bool telefoneExists = connectionString.QueryFirstOrDefault<bool>("SELECT telefone FROM Terceirizados WHERE telefone = @Telefone", new { Telefone = terceirizado.telefone });
    if (telefoneExists) return Results.Conflict("O telefone informado já está cadastrado.");

    try
    {

      connectionString.Query<Terceirizado>("INSERT INTO Terceirizados (nome, funcao, cnpj, telefone, valor, status) VALUES (@Nome, @Funcao, @Cnpj, @Telefone, @Valor, @Status)", new { Nome = terceirizado.nome, Funcao = terceirizado.funcao, Cnpj = terceirizado.cnpj, Telefone = terceirizado.telefone, Valor = terceirizado.valor, Status = terceirizado.status });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}
