static class GetAllClientRepository
{
    public static async Task<PaginatedClients> Get(SqlConnection connectionString, int? pageNumber, int? size, string? search)
    {
        if (size == null) size = 5;

        GetAllClientDto[] clients;
        var totalPages = 0;

        if (search != null)
        {
            clients = (await connectionString.QueryAsync<GetAllClientDto>("SELECT id_cliente, nome_completo, email, cpf, telefone1 FROM Clientes WHERE status = 'true' AND nome_completo LIKE @Search OR status = 'true' AND cpf LIKE @Search ORDER BY id_cliente DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size, Search = $"%{search}%" })).ToArray();
            var clientCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Clientes WHERE status = 'true' AND nome_completo LIKE @Search OR status = 'true' AND cpf LIKE @Search", new { Search = $"%{search}%" });
            totalPages = (int)Math.Ceiling((double)clientCount / (double)size);
        }
        else
        {
            clients = (await connectionString.QueryAsync<GetAllClientDto>("SELECT id_cliente, nome_completo, email, cpf, telefone1 FROM Clientes WHERE status = 'true' ORDER BY id_cliente DESC OFFSET @PageNumber ROWS FETCH NEXT @Size ROWS ONLY", new { PageNumber = (pageNumber - 1) * size, Size = size })).ToArray();
            var clientCount = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Clientes WHERE status = 'true'");
            totalPages = (int)Math.Ceiling((double)clientCount / (double)size);
        }

        return new PaginatedClients(clients: clients, totalPages: totalPages);
    }
}