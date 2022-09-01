static class ClientAlreadyExistsValidator
{
  /** <summary> Esta função verifica se os dados do cliente já existem no banco de dados. </summary>**/
  public static bool Validate(Cliente cliente, SqlConnection connectionString)
  {
    // TODO: Se usuário desativar conta, ao invés criar nova conta, devemos reativar a conta do usuário.
    string storedCpf = connectionString.QueryFirstOrDefault<string>("SELECT cpf FROM Clientes WHERE cpf = @Cpf AND status = 'true'", new { Cpf = cliente.cpf });
    if (storedCpf == cliente.cpf) return false;

    string storedEmail = connectionString.QueryFirstOrDefault<string>("SELECT email FROM Clientes WHERE email = @Email AND status = 'true'", new { Email = cliente.email });
    if (storedEmail == cliente.email) return false;

    string storedCnh = connectionString.QueryFirstOrDefault<string>("SELECT cnh FROM Clientes WHERE cnh = @Cnh AND status = 'true'", new { Cnh = cliente.cnh });
    if (storedCnh == cliente.cnh) return false;

    return true;
  }

  public static bool Validate(int id, Cliente cliente, SqlConnection connectionString)
  {
    string storedCpf = connectionString.QueryFirstOrDefault<string>("SELECT cpf FROM Clientes WHERE cpf = @Cpf AND id_cliente != @Id AND status = 'true'", new { Cpf = cliente.cpf, Id = id });
    if (storedCpf == cliente.cpf) return false;

    string storedEmail = connectionString.QueryFirstOrDefault<string>("SELECT email FROM Clientes WHERE email = @Email AND id_cliente != @Id AND status = 'true'", new { Email = cliente.email, Id = id });
    if (storedEmail == cliente.email) return false;

    string storedCnh = connectionString.QueryFirstOrDefault<string>("SELECT cnh FROM Clientes WHERE cnh = @Cnh AND id_cliente != @Id AND status = 'true'", new { Cnh = cliente.cnh, Id = id });
    if (storedCnh == cliente.cnh) return false;

    return true;
  }
}