static class InsertOutsourcedRepository
{
    public static async Task<Terceirizado?> Insert(Terceirizado outsourced, SqlConnection connectionString)
    {
        try
        {
            await connectionString.QueryAsync("INSERT INTO Terceirizados (nome, funcao, cnpj, telefone, valor) VALUES (@Nome, @Funcao, @Cnpj, @Telefone, @Valor)", new { Nome = outsourced.nome, Funcao = outsourced.funcao, Cnpj = outsourced.cnpj, Telefone = outsourced.telefone, Valor = outsourced.valor });

            var createdOutsourced = await connectionString.QueryFirstOrDefaultAsync<Terceirizado>("SELECT * FROM Terceirizados WHERE cnpj = @Cnpj", new { Cnpj = outsourced.cnpj });
            return createdOutsourced;
        }
        catch (SystemException)
        {
            return null;
        }
    }
}