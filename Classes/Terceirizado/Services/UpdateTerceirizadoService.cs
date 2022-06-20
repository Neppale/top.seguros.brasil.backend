using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
using DocumentValidator;
public static class UpdateTerceirizadoService
{
  /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
  public static IResult Update(int id, Terceirizado terceirizado, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se terceirizado existe.
    bool isExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_terceirizado from Terceirizados WHERE id_terceirizado = @Id", new { Id = id });
    if (!isExistent) return Results.NotFound("Terceirizado não encontrado");

    // Verificando se alguma das propriedades do terceirizado é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Validando CNPJ
    bool cnpjIsValid = CnpjValidation.Validate(terceirizado.cnpj);
    if (!cnpjIsValid) return Results.BadRequest("O CNPJ informado é inválido.");

    try
    {
      connectionString.Query<Terceirizado>("UPDATE Terceirizados SET nome = @Nome, funcao = @Funcao, cnpj = @Cnpj, telefone = @Telefone, valor = @Valor, status = @Status WHERE id_terceirizado = @Id", new { Nome = terceirizado.nome, Funcao = terceirizado.funcao, Cnpj = terceirizado.cnpj, Telefone = terceirizado.telefone, Valor = terceirizado.valor, Status = terceirizado.status, Id = id });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}
