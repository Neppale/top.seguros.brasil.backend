using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
abstract class UpdateStatusApoliceService
{
  /** <summary> Esta função altera o status de uma apólice no banco de dados. </summary>**/
  public static IResult UpdateStatus(int id, Apolice apolice, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do apolice é nula.
    bool hasValidProperties = NullPropertyValidator.Validate(apolice);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificando se apólice existe.
    bool isExistent = connectionString.QueryFirstOrDefault<bool>("SELECT id_apolice from Apolices WHERE id_apolice = @Id", new { Id = id });
    if (!isExistent) return Results.NotFound("Apólice não encontrada.");


    try
    {
      connectionString.Query<Apolice>("UPDATE Apolices SET status = @Status' WHERE id_apolice = @Id", new { Id = id, Status = apolice.status });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}