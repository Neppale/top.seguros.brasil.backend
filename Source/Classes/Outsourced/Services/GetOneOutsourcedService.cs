public static class GetOneOutsourcedService
{
  /** <summary> Esta função retorna um terceirizado específico no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var outsourced = GetOneOutsourcedRepository.Get(id: id, connectionString: connectionString);
    if (outsourced == null) return Results.NotFound("Terceirizado não encontrado.");

    return Results.Ok(outsourced);
  }
}
