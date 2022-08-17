static class GetOneClientService
{
  /** <summary> Esta função retorna um cliente em específico no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var client = GetOneClientRepository.Get(id: id, connectionString: connectionString);
    if (client == null) return Results.NotFound("Cliente não encontrado.");

    return Results.Ok(client);
  }
}