static class GetAllOcorrenciaService
{
  /** <summary> Esta função retorna todas as ocorrências no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Há outro serviço para retorno de documentos. Isso é feito para otimizar o tempo e tamanho da resposta.
    var data = connectionString.Query("SELECT id_ocorrencia, Clientes.nome_completo AS nome, tipo, data, Ocorrencias.status FROM Ocorrencias LEFT JOIN Clientes ON Clientes.id_cliente = Ocorrencias.id_cliente");

    return Results.Ok(data);
  }
}