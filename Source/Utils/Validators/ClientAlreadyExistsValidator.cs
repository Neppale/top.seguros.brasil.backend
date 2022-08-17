static class ClientAlreadyExistsValidator
{
  /** <summary> Esta função verifica se os dados do cliente já existem no banco de dados. </summary>**/
  public static bool Validate(Cliente cliente, SqlConnection connectionString)
  {
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

  public static bool Validate(int id, Cliente cliente, SqlConnection connectionString)
  {
    // Verificando se o CPF já existe no banco de dados.
    string storedCpf = connectionString.QueryFirstOrDefault<string>("SELECT cpf FROM Clientes WHERE cpf = @Cpf AND id_cliente != @Id", new { Cpf = cliente.cpf, Id = id });
    if (storedCpf == cliente.cpf) return false;

    // Verificando se o e-mail já existe no banco de dados.
    string storedEmail = connectionString.QueryFirstOrDefault<string>("SELECT email FROM Clientes WHERE email = @Email AND id_cliente != @Id", new { Email = cliente.email, Id = id });
    if (storedEmail == cliente.email) return false;

    // Verificando se a CNH já existe no banco de dados.
    string storedCnh = connectionString.QueryFirstOrDefault<string>("SELECT cnh FROM Clientes WHERE cnh = @Cnh AND id_cliente != @Id", new { Cnh = cliente.cnh, Id = id });
    if (storedCnh == cliente.cnh) return false;

    return true;
  }
}