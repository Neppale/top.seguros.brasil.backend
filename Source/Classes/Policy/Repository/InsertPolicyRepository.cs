static class InsertPolicyRepository
{
    public static async Task<GetPolicyDto?> Insert(Apolice apolice, SqlConnection connectionString)
    {
        try
        {
            await connectionString.QueryAsync("INSERT INTO Apolices (data_inicio, data_fim, premio, indenizacao, documento, id_cobertura, id_usuario, id_cliente, id_veiculo) VALUES (@DataInicio, @DataFim, @Premio, @Indenizacao, @Documento, @IdCobertura, @IdUsuario, @IdCliente, @IdVeiculo)", new { DataInicio = apolice.data_inicio, DataFim = apolice.data_fim, Premio = apolice.premio, Indenizacao = apolice.indenizacao, Documento = apolice.documento, IdCobertura = apolice.id_cobertura, IdUsuario = apolice.id_usuario, IdCliente = apolice.id_cliente, IdVeiculo = apolice.id_veiculo });

            // Retornando a última apólice inserida.
            var createdPolicy = await connectionString.QueryFirstOrDefaultAsync<GetPolicyDto>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE id_apolice = (SELECT MAX(id_apolice) FROM Apolices)");

            return createdPolicy;
        }
        catch (SystemException)
        {
            return null;
        }
    }
}
