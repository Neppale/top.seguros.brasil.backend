static class GetAllOcorrenciaService
{
  /** <summary> Esta função retorna todas as ocorrências no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString, int? pageNumber)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Se pageNumber for nulo, então a página atual é a primeira.
    if (pageNumber == null) pageNumber = 1;

    // Há outro serviço para retorno de documentos. Isso é feito para otimizar o tempo e tamanho da resposta.
    var data = connectionString.Query("SELECT id_ocorrencia, Clientes.nome_completo AS nome, tipo, data, Ocorrencias.status FROM Ocorrencias LEFT JOIN Clientes ON Clientes.id_cliente = Ocorrencias.id_cliente ORDER BY id_ocorrencia OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });

    return Results.Ok(data);
  }
}