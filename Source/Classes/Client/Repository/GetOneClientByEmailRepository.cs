static class GetOneClientByEmailRepository
{
  public static GetOneClientDto Get(string email, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<GetOneClientDto>("SELECT id_cliente, nome_completo, email, telefone1, telefone2, cnh, cep, cpf, data_nascimento FROM Clientes WHERE email = @Email AND status = 'true'", new { Email = email });
  }
}