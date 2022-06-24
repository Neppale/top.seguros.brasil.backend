static class DeleteUsuarioService
{
  public static IResult Delete(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se o usuário existe.
    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_usuario FROM Usuarios WHERE id_usuario = @Id AND status = 'true'", new { Id = id });
    if (!Exists) return Results.NotFound("Usuário não encontrado.");

    try
    {
      connectionString.Query("UPDATE Usuarios SET status = 'false' WHERE id_Usuario = @Id", new { Id = id });
      return Results.StatusCode(204);
    }
    catch (SystemException)
    {

      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}