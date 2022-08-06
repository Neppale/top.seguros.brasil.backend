static class InsertPolicyRepository
{
  public static async Task<int> Insert(Apolice apolice, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("INSERT INTO Apolices (data_inicio, data_fim, premio, indenizacao, documento, id_cobertura, id_usuario, id_cliente, id_veiculo) VALUES (@DataInicio, @DataFim, @Premio, @Indenizacao, @Documento, @IdCobertura, @IdUsuario, @IdCliente, @IdVeiculo)", new { DataInicio = apolice.data_inicio, DataFim = apolice.data_fim, Premio = apolice.premio, Indenizacao = apolice.indenizacao, Documento = apolice.documento, IdCobertura = apolice.id_cobertura, IdUsuario = apolice.id_usuario, IdCliente = apolice.id_cliente, IdVeiculo = apolice.id_veiculo });

      // Retornando o id da apólice inserida.
      int createdApoliceId = connectionString.QueryFirstOrDefault<int>("SELECT id_apolice FROM Apolices WHERE id_cliente = @IdCliente AND id_veiculo = @IdVeiculo AND data_inicio = @DataInicio AND data_fim = @DataFim", new { IdCliente = apolice.id_cliente, IdVeiculo = apolice.id_veiculo, DataInicio = apolice.data_inicio, DataFim = apolice.data_fim });

      apolice.id_apolice = createdApoliceId;

      // Gerando documento da apólice.
      string filePath = await PolicyDocumentGenerator.Generate(apolice: apolice, connectionString: connectionString);

      // Lendo documento no local específicado.
      Stream fileStream = File.OpenRead(filePath);

      // Convertendo documento para base64.
      string document = DocumentConverter.Encode(stream: fileStream);

      // Inserindo documento da apólice no banco de dados.
      connectionString.Query("UPDATE Apolices SET documento = @Documento WHERE id_apolice = @IdApolice", new { Documento = document, IdApolice = createdApoliceId });

      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}