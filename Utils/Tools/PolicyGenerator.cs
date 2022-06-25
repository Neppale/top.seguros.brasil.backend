static class PolicyGenerator
{
  /** <summary> Esta função gera um valor de indenização. </summary>**/
  public static async Task<decimal> GenerateIndenizacao(int id_veiculo, string dbConnectionString)
  {
    SqlConnection connection = new SqlConnection(dbConnectionString);

    // Recuperar dados do veículo no banco.
    Veiculo veiculo = connection.QueryFirst<Veiculo>("SELECT * FROM Veiculos WHERE id_veiculo = @id", new { id = id_veiculo });

    decimal indenizationValue = await FipeAPIAccess.GetValue(veiculo.marca, veiculo.modelo, veiculo.ano);
    return indenizationValue;

  }
  /** <summary> Esta função gera um valor de prêmio. </summary>**/
  public static async Task<decimal> GeneratePremio(int id_veiculo, string dbConnectionString)
  {
    SqlConnection connection = new SqlConnection(dbConnectionString);

    // Recuperar dados do veículo no banco.
    Veiculo veiculo = connection.QueryFirst<Veiculo>("SELECT * FROM Veiculos WHERE id_veiculo = @id", new { id = id_veiculo });

    // O prêmio consiste em apenas 1% do valor do veículo.
    decimal value = await FipeAPIAccess.GetValue(veiculo.marca, veiculo.modelo, veiculo.ano);
    decimal premiumValue = value * 0.01m;
    premiumValue = Math.Round(premiumValue, 2);

    return premiumValue;
  }
  public static int ChooseUsuario(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Retornar o usuário que possuir menos apólices associadas à ele.
    //TODO: Usuário com status inativo não deve ser escolhido.
    int leastApoliceUsuarioId = connectionString.QueryFirstOrDefault<int>("SELECT Usuarios.id_usuario, COUNT(Apolices.id_apolice) AS apolices FROM Usuarios LEFT JOIN Apolices ON Apolices.id_usuario = Usuarios.id_usuario GROUP BY Usuarios.id_usuario ORDER BY apolices ASC");

    return leastApoliceUsuarioId;

  }
}