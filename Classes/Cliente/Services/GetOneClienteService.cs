static class GetOneClienteService
{
  /** <summary> Esta função retorna um cliente em específico no banco de daods. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.QueryFirstOrDefault("SELECT id_cliente, nome_completo, email, cpf, cnh, cep, data_nascimento, telefone1, telefone2, status FROM Clientes WHERE id_cliente = @Id", new { Id = id });
    if (data == null) return Results.NotFound("Cliente não encontrado.");

    return Results.Ok(data);
  }
}