static class GetAllClientRepository
{
    public static async Task<IEnumerable<GetAllClientDto>> Get(SqlConnection connectionString, int? pageNumber, int? size)
    {
        return await connectionString.QueryAsync<GetAllClientDto>("SELECT id_cliente, nome_completo, email, cpf, telefone1 FROM Clientes WHERE status = 'true' ORDER BY id_cliente DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size });
    }
}