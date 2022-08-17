static class GetOneClientRepository
{
  /** <summary> Esta função retorna um cliente em específico no banco de dados. </summary>**/
  public static dynamic Get(int id, SqlConnection connectionString)
  {
    var client = connectionString.QueryFirstOrDefault<dynamic>("SELECT id_cliente, nome_completo, email, cpf, cnh, cep, data_nascimento, telefone1, telefone2 FROM Clientes WHERE id_cliente = @Id AND status = 'true'", new { Id = id });

    return client;
  }
}