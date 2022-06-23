using Dapper;
using Microsoft.Data.SqlClient;

static class PolicyGenerator
{
  public static float GenerateIndenizacao(int id_veiculo)
  {
    // TODO: Implementar o cálculo da indenização.
    return 1.00f;
  }
  public static float GeneratePremio(int id_veiculo)
  {
    // TODO: Implementar o cálculo do prêmio.
    return 1.00f;
  }
  public static int ChooseUsuario(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Retornar o usuário que possuir menos apólices associadas à ele.
    int leastApoliceUsuarioId = connectionString.QueryFirstOrDefault<int>("SELECT Usuarios.id_usuario, COUNT(Apolices.id_apolice) AS apolices FROM Usuarios JOIN Apolices ON Apolices.id_usuario = Usuarios.id_usuario GROUP BY Usuarios.id_usuario ORDER BY apolices ASC");

    return leastApoliceUsuarioId;

  }
}