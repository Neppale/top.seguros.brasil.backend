static class GetApolicesByUsuarioService
{
  public static IResult Get(int id_usuario, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Apolice>("SELECT * from Apolices WHERE id_usuario = @id_usuario", new { id_usuario });
    if (data.Count() == 0) return Results.NotFound("Nenhuma apólice encontrada para o usuário, ou usuário não existe.");

    return Results.Ok(data);
  }
}