public static class GetOneVeiculoService
{
  /** <summary> Esta função retorna um veículo específico no banco de dados. </summary>**/
  public static IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.QueryFirstOrDefault("SELECT * FROM Veiculos WHERE id_Veiculo = @Id", new { Id = id });
    if (data == null) return Results.NotFound("Veículo não encontrado.");

    // Removendo caracteres especiais da exibição do modelo do veículo.
    data.modelo = VehicleModelUnformatter.Unformat(data.modelo);

    return Results.Ok(data);
  }
}