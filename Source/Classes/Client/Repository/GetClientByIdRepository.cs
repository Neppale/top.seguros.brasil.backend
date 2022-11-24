static class GetClientByIdRepository
{
    /** <summary> Esta função retorna um cliente em específico no banco de dados. </summary>**/
    public static async Task<GetClientDto> Get(int id, SqlConnection connectionString)
    {
        var client = await connectionString.QueryFirstOrDefaultAsync<GetClientDto>("SELECT id_cliente, nome_completo, email, cpf, cnh, cep, data_nascimento, telefone1, telefone2 FROM Clientes WHERE id_cliente = @Id AND status = 'true'", new { Id = id });
        client.data_nascimento = SqlDateConverter.ConvertToShow(client.data_nascimento);
        return client;
    }
}