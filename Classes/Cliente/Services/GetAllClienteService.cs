using Dapper;
using Microsoft.Data.SqlClient;
abstract class GetAllClienteService
{
  /** <summary> Esta função retorna todos os clientes no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query("SELECT id_cliente, nome_completo, email, cpf, cnh, cep, data_nascimento, telefone1, telefone2, status FROM Clientes WHERE status = 'true'");

    return Results.Ok(data);
  }
}