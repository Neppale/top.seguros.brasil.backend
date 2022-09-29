static class UserSelector
{
  static public async Task<int> Select(SqlConnection connectionString)
  {
    int userWithLeastPolicies = await connectionString.QueryFirstOrDefaultAsync<int>("SELECT Usuarios.id_usuario, COUNT(Apolices.id_apolice) AS apolices FROM Usuarios LEFT JOIN Apolices ON Usuarios.id_usuario = Apolices.id_usuario WHERE Usuarios.status = 'true' AND Usuarios.tipo = 'Corretor' GROUP BY Usuarios.id_usuario ORDER BY apolices ASC");

    return userWithLeastPolicies;

  }
}