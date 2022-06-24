namespace tsb.mininal.policy.engine.Utils
{
  public static class Healthcheck
  {
    /** <summary> Esta função faz uma checagem da conexão com o banco de dados. </summary>**/
    public static IResult Check(string dbConnectionString)
    {
      SqlConnection connectionString = new SqlConnection(dbConnectionString);

      if (!connectionString.QuerySingleOrDefault<bool>($"SELECT * from Clientes WHERE id_cliente = 1")) return Results.StatusCode(503);

      if (!connectionString.QuerySingleOrDefault<bool>($"SELECT * from Coberturas  WHERE id_cobertura = 1")) return Results.StatusCode(503);

      if (!connectionString.QuerySingleOrDefault<bool>($"SELECT * from Usuarios WHERE id_usuario = 1")) return Results.StatusCode(503);

      if (!connectionString.QuerySingleOrDefault<bool>($"SELECT * from Veiculos WHERE id_veiculo = 1")) return Results.StatusCode(503);

      if (!connectionString.QuerySingleOrDefault<bool>($"SELECT * from Terceirizados WHERE id_terceirizado = 1")) return Results.StatusCode(503);

      if (!connectionString.QuerySingleOrDefault<bool>($"SELECT * from Apolices WHERE id_apolice = 1")) return Results.StatusCode(503);

      if (!connectionString.QuerySingleOrDefault<bool>($"SELECT id_cobertura from Coberturas WHERE id_cobertura = 1")) return Results.StatusCode(503);

      return Results.Ok();
    }
  }
}
