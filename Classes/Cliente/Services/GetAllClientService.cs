static class GetAllClientService
{
  /** <summary> Esta função retorna todos os clientes no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString, int? pageNumber)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;


    var data = connectionString.Query("SELECT id_cliente, nome_completo, email, cpf, telefone1 FROM Clientes WHERE status = 'true' ORDER BY id_cliente OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });

    return Results.Ok(data);
  }
}