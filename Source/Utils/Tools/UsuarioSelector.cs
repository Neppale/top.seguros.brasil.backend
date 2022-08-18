static class UsuarioSelector
{
  static public int Select(SqlConnection connectionString)
  {
    int userWithLeastPolicies = connectionString.QueryFirstOrDefault<int>("SELECT Usuarios.id_usuario, COUNT(Apolices.id_apolice) AS apolices FROM Usuarios LEFT JOIN Apolices ON Usuarios.id_usuario = Apolices.id_usuario WHERE Usuarios.status = 'true' GROUP BY Usuarios.id_usuario ORDER BY apolices ASC");

    return userWithLeastPolicies;

  }
}