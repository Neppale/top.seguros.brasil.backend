public static class GetOutsourcedByIdService
{
  /** <summary> Esta função retorna um terceirizado específico no banco de dados. </summary>**/
  public static IResult Get(int id, SqlConnection connectionString)
  {
    var outsourced = GetOutsourcedByIdRepository.Get(id: id, connectionString: connectionString);
    if (outsourced == null) return Results.NotFound(new { message = "Terceirizado não encontrado." });

    return Results.Ok(outsourced);
  }
}
