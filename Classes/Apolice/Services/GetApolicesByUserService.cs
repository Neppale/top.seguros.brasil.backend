static class GetApolicesByUsuarioService
{
  public static IResult Get(int id_usuario, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query("SELECT id_apolice, Clientes.nome_completo AS nome, Coberturas.nome AS tipo, Veiculos.modelo AS veiculo, Apolices.status FROM Apolices LEFT JOIN Clientes ON Clientes.id_cliente = Apolices.id_cliente LEFT JOIN Coberturas ON Coberturas.id_cobertura = Apolices.id_cobertura LEFT JOIN Veiculos ON Veiculos.id_veiculo = Apolices.id_veiculo WHERE Apolices.id_usuario = @Id", new { Id = id_usuario });
    if (data.Count() == 0) return Results.NotFound("Nenhuma apólice encontrada para o usuário, ou usuário não existe.");

    return Results.Ok(data);
  }
}