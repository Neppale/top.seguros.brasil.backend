static class ClientAlreadyExistsValidator
{
  /** <summary> Esta função verifica se os dados do cliente já existem no banco de dados. </summary>**/
  public static bool Validate(Cliente cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se o CPF já existe no banco de dados.
    string storedCpf = connectionString.QueryFirstOrDefault<string>("SELECT cpf FROM Clientes WHERE cpf = @Cpf", new { Cpf = cliente.cpf });
    if (storedCpf == cliente.cpf) return false;

    // Verificando se o e-mail já existe no banco de dados.
    string storedEmail = connectionString.QueryFirstOrDefault<string>("SELECT email FROM Clientes WHERE email = @Email", new { Email = cliente.email });
    if (storedEmail == cliente.email) return false;

    // Verificando se a CNH já existe no banco de dados.
    string storedCnh = connectionString.QueryFirstOrDefault<string>("SELECT cnh FROM Clientes WHERE cnh = @Cnh", new { Cnh = cliente.cnh });
    if (storedCnh == cliente.cnh) return false;

    return true;
  }
}