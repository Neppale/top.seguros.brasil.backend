static class GetOnePolicyService
{
  /** <summary> Esta função retorna uma apólice específica no banco de dados. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault<Apolice>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE id_apolice = @Id", new { Id = id });

    if (data == null) return Results.NotFound("Apólice não encontrada.");

    return Results.Ok(data);
  }
}