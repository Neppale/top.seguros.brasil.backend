using Dapper;
using Microsoft.Data.SqlClient;
abstract class GetAllOcorrenciaService
{
  /** <summary> Esta função retorna todas as ocorrências no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    //TODO: Retornar documento das ocorrências.
    var data = connectionString.Query<Ocorrencia>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias");

    return Results.Ok(data);
  }
}