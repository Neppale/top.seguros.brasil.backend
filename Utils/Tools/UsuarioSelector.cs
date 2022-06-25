static class UsuarioSelector
{
  static public int Select(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Retornar o usuário que possuir menos apólices associadas à ele.
    //TODO: Usuário com status inativo não deve ser escolhido.
    int leastApoliceUsuarioId = connectionString.QueryFirstOrDefault<int>("SELECT p.id_usuario, COUNT(o.id_apolice) AS apolices FROM Usuarios p LEFT JOIN Apolices o ON p.id_usuario = o.id_usuario WHERE p.status != 'false' GROUP BY p.id_usuario ORDER BY apolices ASC");

    return leastApoliceUsuarioId;

  }
}