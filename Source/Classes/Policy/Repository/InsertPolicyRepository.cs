static class InsertPolicyRepository
{
    public static async Task<GetPolicyDto?> Insert(Apolice apolice, SqlConnection connectionString)
    {
        try
        {
            await connectionString.QueryAsync("INSERT INTO Apolices (data_inicio, data_fim, premio, indenizacao, documento, id_cobertura, id_usuario, id_cliente, id_veiculo) VALUES (@DataInicio, @DataFim, @Premio, @Indenizacao, @Documento, @IdCobertura, @IdUsuario, @IdCliente, @IdVeiculo)", new { DataInicio = apolice.data_inicio, DataFim = apolice.data_fim, Premio = apolice.premio, Indenizacao = apolice.indenizacao, Documento = apolice.documento, IdCobertura = apolice.id_cobertura, IdUsuario = apolice.id_usuario, IdCliente = apolice.id_cliente, IdVeiculo = apolice.id_veiculo });

            // Retornando a última apólice inserida.
            var createdPolicy = await connectionString.QueryFirstOrDefaultAsync<GetPolicyDto>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE id_apolice = (SELECT MAX(id_apolice) FROM Apolices)");

            // Gerando documento da apólice.
            string filePath = await PolicyDocumentGenerator.Generate(apolice: apolice, connectionString: connectionString);
            // Lendo documento no local específicado.
            Stream fileStream = File.OpenRead(filePath);

            // Convertendo documento para base64.
            string document = DocumentConverter.Encode(stream: fileStream);

            // Inserindo documento da apólice no banco de dados.
            await connectionString.QueryAsync("UPDATE Apolices SET documento = @Documento WHERE id_apolice = @IdApolice", new { Documento = document, IdApolice = createdPolicy.id_apolice });

            return createdPolicy;
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("[ERRO] Diretório não encontrado, porém apólice foi criada.");
            return new GetPolicyDto();
        }
        catch (SystemException)
        {
            return null;
        }
    }
}
