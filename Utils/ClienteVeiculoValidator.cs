static class ClienteVeiculoValidator
{
  /** <summary> Esta função verifica se o veículo escolhido pertence ao cliente escolhido. </summary>**/
  public static bool Validate(int id_cliente, int id_veiculo, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    int veiculoClientId = connectionString.QueryFirstOrDefault<int>("SELECT id_cliente from Veiculos WHERE id_veiculo = @Id", new { Id = id_veiculo });
    if (veiculoClientId != id_cliente) return false;
    return true;
  }
}