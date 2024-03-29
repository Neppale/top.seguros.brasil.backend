static class InsertClientRepository
{
    public static async Task<GetClientDto?> Insert(Cliente cliente, SqlConnection connectionString)
    {
        try
        {
            await connectionString.QueryAsync("INSERT INTO Clientes (email, senha, nome_completo, cpf, cnh, cep, data_nascimento, telefone1, telefone2) VALUES (@Email, @Senha, @Nome, @Cpf, @Cnh, @Cep, @DataNascimento, @Telefone1, @Telefone2)", new { Email = cliente.email, Senha = cliente.senha, Nome = cliente.nome_completo, Cpf = cliente.cpf, Cnh = cliente.cnh, Cep = cliente.cep, DataNascimento = cliente.data_nascimento, Telefone1 = cliente.telefone1, Telefone2 = cliente.telefone2 });

            var client = await connectionString.QueryFirstOrDefaultAsync<GetClientDto>("SELECT * FROM Clientes WHERE email = @Email", new { Email = cliente.email });
            return client;
        }
        catch (SystemException exception)
        {
            Console.WriteLine($"[WHAT HAVE YOU DONE?] {exception.Message}");
            return null;
        }
    }
}