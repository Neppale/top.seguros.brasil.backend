static class GetAllOcorrenciaService
{
  /** <summary> Esta função retorna todas as ocorrências no banco de dados. </summary>**/
  public static IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Há outro serviço para retorno de documentos. Isso é feito para otimizar o tempo e tamanho da resposta.
    var data = connectionString.Query<Ocorrencia>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias");

    return Results.Ok(data);
  }
}