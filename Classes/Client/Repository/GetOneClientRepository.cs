static class GetOneClientRepository
{
  /** <summary> Esta função retorna um cliente em específico no banco de dados. </summary>**/
  public static Cliente Get(int id, SqlConnection connectionString)
  {
    var client = connectionString.QueryFirstOrDefault<Cliente>("SELECT id_cliente, nome_completo, email, cpf, cnh, cep, data_nascimento, telefone1, telefone2 FROM Clientes WHERE id_cliente = @Id", new { Id = id });

    return client;
  }
}