static class GetOneOutsourcedRepository
{
  public static Terceirizado Get(int id, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<Terceirizado>("SELECT * FROM Terceirizados WHERE id_terceirizado = @Id", new { Id = id });
  }
}