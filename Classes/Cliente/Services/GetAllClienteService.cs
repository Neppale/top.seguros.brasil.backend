static class GetAllClienteService
{
  /** <summary> Esta função retorna todos os clientes no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query("SELECT id_cliente, nome_completo, email, cpf, telefone1 FROM Clientes WHERE status = 'true'");

    return Results.Ok(data);
  }
}