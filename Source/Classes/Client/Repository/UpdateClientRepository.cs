static class UpdateClientRepository
{
  public static async Task<GetClientDto?> Update(int id, Cliente cliente, SqlConnection connectionString)
  {
    try
    {
      await connectionString.QueryAsync("UPDATE Clientes SET email = @Email, senha = @Senha, nome_completo = @Nome, cnh = @Cnh, cep = @Cep, data_nascimento = @DataNascimento, telefone1 = @Telefone1, telefone2 = @Telefone2 WHERE id_cliente = @Id",
        new { Email = cliente.email, Senha = cliente.senha, Nome = cliente.nome_completo, Cnh = cliente.cnh, Cep = cliente.cep, DataNascimento = cliente.data_nascimento, Telefone1 = cliente.telefone1, Telefone2 = cliente.telefone2, Id = id });

      var client = await connectionString.QueryFirstOrDefaultAsync<GetClientDto>("SELECT * FROM Clientes WHERE id_cliente = @Id", new { Id = id });
      return client;
    }
    catch (SystemException)
    {
      return null;
    }
  }
}