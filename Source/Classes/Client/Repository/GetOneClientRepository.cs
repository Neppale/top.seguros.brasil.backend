static class GetOneClientRepository
{
  /** <summary> Esta função retorna um cliente em específico no banco de dados. </summary>**/
  public static GetOneClientDto Get(int id, SqlConnection connectionString)
  {
    return connectionString.QueryFirstOrDefault<GetOneClientDto>("SELECT id_cliente, nome_completo, email, cpf, cnh, cep, data_nascimento, telefone1, telefone2 FROM Clientes WHERE id_cliente = @Id AND status = 'true'", new { Id = id });
  }
}