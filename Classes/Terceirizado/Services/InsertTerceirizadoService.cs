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

    try
    {
      // Verificando se alguma das propriedades do Terceirizado é nula ou vazia.
      bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      // Validando CNPJ
      bool cnpjIsValid = CnpjValidation.Validate(terceirizado.cnpj);
      if (!cnpjIsValid) return Results.BadRequest("O CNPJ informado é inválido.");

      var data = connectionString.Query<Terceirizado>("INSERT INTO Terceirizados (nome, funcao, cnpj, telefone, valor, status) VALUES (@Nome, @Funcao, @Cnpj, @Telefone, @Valor, @Status)", new { Nome = terceirizado.nome, Funcao = terceirizado.funcao, Cnpj = terceirizado.cnpj, Telefone = terceirizado.telefone, Valor = terceirizado.valor, Status = terceirizado.status });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}
