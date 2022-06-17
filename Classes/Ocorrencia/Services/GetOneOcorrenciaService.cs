using Dapper;
using Microsoft.Data.SqlClient;

abstract class GetOneOcorrenciaService
{
  /** <summary> Esta função retorna uma ocorrência específica no banco de dados. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault<Ocorrencia>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });

    if (data == null) return Results.NotFound("Ocorrência não encontrada.");

    return Results.Ok(data);
  }
}