public static class DeleteOutsourcedService
{
  /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
  public static IResult Delete(int id, SqlConnection connectionString)
  {
    // Verificando se terceirizado existe.
    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_terceirizado from Terceirizados WHERE id_terceirizado = @Id AND status = 'true'", new { Id = id });
    if (!Exists) return Results.NotFound("Terceirizado não encontrado");

    try
    {
      connectionString.Query("UPDATE Terceirizados SET status = 'false' WHERE id_terceirizado = @Id", new { Id = id });
      return Results.NoContent();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");
    }

  }
}
