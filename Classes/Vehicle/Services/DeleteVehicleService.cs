public static class DeleteVehicleService
{
  /** <summary> Esta função deleta um Veículo no banco de dados. </summary>**/
  public static IResult Delete(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se veículo existe.
    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_veiculo from Veiculos WHERE id_Veiculo = @Id", new { Id = id });
    if (!Exists) return Results.NotFound("Veículo não encontrado.");

    try
    {
      connectionString.Query("UPDATE Veiculos SET status = 'false' WHERE id_veiculo = @Id", new { Id = id });

      return Results.NoContent();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");
    }

  }
}