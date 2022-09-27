static class GetPolicyByIdRepository
{
    public static async Task<GetPolicyDto> Get(int id, SqlConnection connectionString)
    {
        return await connectionString.QueryFirstOrDefaultAsync<GetPolicyDto>("SELECT id_apolice, data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status from Apolices WHERE id_apolice = @Id", new { Id = id });
    }
}