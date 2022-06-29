static class GetAllOcorrenciasByClienteService
{
  public static IResult Get(int id_cliente, string dbConnectionString, int? pageNumber)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    var data = connectionString.Query("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, Veiculos.modelo AS veiculo, Terceirizados.nome as terceirizado, Ocorrencias.status FROM Ocorrencias LEFT JOIN Veiculos ON Veiculos.id_veiculo = Ocorrencias.id_veiculo LEFT JOIN Terceirizados ON Terceirizados.id_terceirizado = Ocorrencias.id_terceirizado WHERE Ocorrencias.id_cliente = @IdCliente", new { PageNumber = (pageNumber - 1) * 5, IdCliente = id_cliente });

    return Results.Ok(data);
  }
}