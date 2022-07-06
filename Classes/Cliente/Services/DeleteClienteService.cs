static class DeleteClienteService
{
  public static IResult Delete(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se o cliente existe.
    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_cliente FROM Clientes WHERE id_cliente = @Id AND status = 'true'", new { Id = id });
    if (!Exists) return Results.NotFound("Cliente não encontrado.");

    try
    {
      connectionString.Query("UPDATE Clientes SET status = 'false' WHERE id_cliente = @Id", new { Id = id });
      return Results.NoContent();
    }
    catch (SystemException)
    {

      return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");
    }

  }
}