static class GetClientByEmailRepository
{
  public static dynamic Get(string email, SqlConnection connectionString)
  {
    var client = connectionString.QueryFirstOrDefault("SELECT id_cliente, nome_completo, email, telefone1, telefone2, cnh, cep, cpf, data_nascimento FROM Clientes WHERE email = @Email AND status = 'true'", new { Email = email });
    return client;
  }
}